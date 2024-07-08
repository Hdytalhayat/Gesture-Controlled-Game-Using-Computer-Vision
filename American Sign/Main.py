import cv2
import cvzone
from cvzone.FPS import FPS
from cvzone.HandTrackingModule import HandDetector
from cvzone.ClassificationModule import Classifier
from cvzone.FaceMeshModule import FaceMeshDetector
import socket
import pyautogui
import numpy as np
import math
import time

classifier = Classifier("Model/keras_model.h5","Model/labels.txt")
labels = ["A", "B", "C", "D", "F", "G", "H", "I", "J", "K", "L", "M",
          "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "Y", "Z", "E", "X"]

offset = 20
imgSize = 400

folder = "Data/C"
counter = 0


isPressed = False
counterPressed = 0
delayPressed = 5

# Communication setup
sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
serverAddressPort = ("127.0.0.1", 1209)

# Initialize
cap = cv2.VideoCapture(0)
fpsReader = FPS(avgCount=60)
cap.set(cv2.CAP_PROP_FPS, 60)  # Set the frames per second to 30

x = [300,245,200,170,145,130,112,103,93,87,80,75,70,67,62,59,57]
y = [20,25,30,35,40,45,50,55,60,65,70,75,80,85,90,95,100]
coff = np.polyfit(x,y,2)
A,B,C = coff
fingers1 = [0,0,0,0,0]
# Initialize detectors
handDetector = HandDetector(maxHands=1)
faceDetector = FaceMeshDetector(maxFaces=1)

# Define landmarks for eyes and mouth
eyesList = [22, 23, 24, 26, 110, 157, 158, 159, 160, 161, 130, 243]
mouthList = [0, 17, 78, 292]

while True:
    success, img = cap.read()
    # if not success:
    #     break

    # Detect face mesh
    img, faces = faceDetector.findFaceMesh(img, draw=False)

    # Detect hands
    hands, img = handDetector.findHands(img, draw=True)

    # Initialize flags and data
    isSmile = IsOpenMouth = False
    nose = (0, 0)
    fingers1_count = fingers2_count = 0

    isRight = False
    faceDistanceCM = 0
    hand1DistanceCM = 0
    hand2DistanceCM = 0
    UpControl = False
    DownControl = False
    asl = ""
    if faces:
        # mencari angka pada wajah
        
        face = faces[0]
        # for idNo, point in enumerate(face):
        #     cv2.putText(img, str(idNo), point, cv2.FONT_HERSHEY_COMPLEX_SMALL, 0.8, (0,255,0), 1)
        facex1,facey1 = face[103]
        facex2,facey2 = face[332]
        faceDistance = int(math.sqrt((facey2-facey1)**2 +(facex2-facex1)**2))
        
        faceDistanceCM = A*faceDistance**2 + B*faceDistance + C

        # print(faceDistanceCM)
        # Eye detection
        for id in eyesList:
            cv2.circle(img, face[id], 5, (255, 0, 255), cv2.FILLED)

        eyeUp = face[159]
        eyeBottom = face[23]
        eyeLeft = face[130]
        eyeRight = face[243]
        eyeLengthVertical, _ = faceDetector.findDistance(eyeUp, eyeBottom)
        eyeLengthHorizontal, _ = faceDetector.findDistance(eyeLeft, eyeRight)

        cv2.line(img, eyeUp, eyeBottom, (0, 255, 0), 3)
        cv2.line(img, eyeLeft, eyeRight, (0, 255, 0), 3)

        checkEyeLength = int((eyeLengthVertical / eyeLengthHorizontal) * 100)
        IsBlink = checkEyeLength < 38
        # print(checkEyeLength)
        # Mouth detection
        for id in mouthList:
            cv2.circle(img, face[id], 5, (255, 0, 255), cv2.FILLED)

        lipUp = face[0]
        lipBottom = face[17]
        lipRight = face[78]
        lipLeft = face[292]
        cv2.line(img, lipUp, lipBottom, (0, 255, 0), 3)
        cv2.line(img, lipLeft, lipRight, (0, 255, 0), 3)
        
        lipLengthVertical, _ = faceDetector.findDistance(lipUp, lipBottom)
        lipLengthHorizontal, _ = faceDetector.findDistance(lipRight, lipLeft)
        checkLipLength = int((lipLengthVertical / lipLengthHorizontal) * 100)

        isSmile = lipLengthHorizontal > 40 and checkEyeLength < 30
        IsOpenMouth = checkLipLength > 70

        # Nose detection
        nose = face[1]
        cv2.circle(img, nose, 5, (255, 0, 255), cv2.FILLED)

    if hands:
        try:
            hand = hands[0]
            x, y, w, h = hand['bbox']

            imgWhite = np.ones((imgSize,imgSize,3),np.uint8)*255
            imgCrop = img[y-offset:y+h+offset,x-offset:x+w+offset]
            imgCropShape = imgCrop.shape
            
            aspectRatio = h/w
            

            if aspectRatio > 1:
                k = imgSize/h
                wCal = math.ceil(k * w)
                imgResize = cv2.resize(imgCrop,(wCal,imgSize))
                imgResizeShape = imgResize.shape
                wGap = math.ceil((imgSize-wCal)/2)
                imgWhite[:,wGap:wCal+wGap] = imgResize
                prediction, index = classifier.getPrediction(imgWhite,draw=False)
                # print(prediction, index)
            else:
                k = imgSize/w
                hCal = math.ceil(k * h)
                imgResize = cv2.resize(imgCrop,(imgSize,hCal))
                imgResizeShape = imgResize.shape
                hGap = math.ceil((imgSize-hCal)/2)
                imgWhite[hGap:hGap+hCal] = imgResize    
                prediction, index = classifier.getPrediction(imgWhite,draw=False)
                # print(prediction, index)

            # cv2.imshow("ImageCrop", imgCrop)
            # cv2.imshow("ImageWhite", imgWhite)
            asl = labels[index]
            img = cv2.putText(img, labels[index], (x,y-20), cv2.FONT_HERSHEY_COMPLEX,2,(255,0,255),2)
        except:
            print("Tangan terlalu dekat!")
        
    fps, img = fpsReader.update(img, pos=(20, 50),
                                bgColor=(255, 0, 255), textColor=(255, 255, 255),
                                scale=3, thickness=3)

    if isRight:
        data = [asl, IsOpenMouth, nose, int(faceDistanceCM), fingers1_count, fingers2_count, int(hand1DistanceCM), int(hand2DistanceCM), UpControl, DownControl]
    else:
        data = [asl, IsOpenMouth, nose, int(faceDistanceCM), fingers2_count, fingers1_count, int(hand2DistanceCM), int(hand1DistanceCM), UpControl, DownControl]

    print(data)
    sock.sendto(str.encode(str(data)), serverAddressPort)
    img = cv2.resize(img, (800, 600))
    cv2.imshow("Image", img)

    cv2.waitKey(1)
    
    if isPressed:
        counterPressed += 1
        if counterPressed > delayPressed:
            counterPressed = 0
            isPressed = False


cap.release()
cv2.destroyAllWindows()


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Cam : MonoBehaviour
{
    float camX;
    float camY;
    float rotateCamX;
    float rotateCamY;
    public float mouseSentivity = 100f;
    bool timeRemaining;
    public Slider sensiSlider;

    UDPReceive uDPReceive;
    string data;
    string[] points;


    // Start is called before the first frame update

    private void Start()
    {

        uDPReceive = GetComponent<UDPReceive>();
        data = uDPReceive.data;

        if (PlayerPrefs.HasKey("Sensitivity"))
        {
            loadSensi();
        }
        else
        {
            PlayerPrefs.SetFloat("Sensitivity", 200);
            loadSensi();
        }
    }

    void Awake()
    {
        transform.Rotate(0, 0, 0);
        timeRemaining = true;
        //GameObject.Find("GameHandler").GetComponent<Timer_Script>().on_Time_finished += onTimeFinished;
    }

    // Update is called once per frame
    void Update()
    {
        DataHandle();

        if (timeRemaining)
        {
            camX += Input.GetAxis("Mouse X") * mouseSentivity * Time.deltaTime;
            camY += Input.GetAxis("Mouse Y") * mouseSentivity * Time.deltaTime;
            camY = Mathf.Clamp(camY, -90, 90);
            camX = Mathf.Clamp(camX, -90, 90);
            //transform.Rotate(rotateCamX,rotateCamY, 0); 
            //transform.localEulerAngles = new Vector3(rotateCamX, 0, 0);
            transform.localRotation = Quaternion.Euler(-camY, camX, 0);
        }
    }

    private void DataHandle()
    {
        data = uDPReceive.data;
        // Remove the first and last character
        data = data.Remove(0, 1);
        data = data.Remove(data.Length - 1, 1);
        points = data.Split(", ");
        // print(points[0]+""+ points[1]); // Print the first point for debugging
    }

    void onTimeFinished()
    {
        timeRemaining = false;
    }

    public void setSentivity()
    {
        mouseSentivity = sensiSlider.value;
        saveSensi();
    }

    void loadSensi()
    {
        sensiSlider.value = PlayerPrefs.GetFloat("Sensitivity");
    }

    void saveSensi()
    {
        PlayerPrefs.SetFloat("Sensitivity", sensiSlider.value);
    }
}

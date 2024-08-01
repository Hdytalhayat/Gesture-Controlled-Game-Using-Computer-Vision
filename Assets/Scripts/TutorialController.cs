using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    public Sprite tutorialSprite;
    public Image TutorialImg;
    public GameObject udp, tutorial;
    public bool IsTutorial = true;

    UDPReceive uDPReceive;
    string data;
    string[] points;
    
    void Start()
    {
        TutorialImg.sprite = tutorialSprite;
        uDPReceive = udp.GetComponent<UDPReceive>();
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
    void  Update()
    {
        DataHandle();
        if(points[9] == "'enter'")
        {
            tutorial.SetActive(false);
        }
    }
}

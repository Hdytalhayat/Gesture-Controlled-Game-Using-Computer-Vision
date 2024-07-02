using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class TesJarak : MonoBehaviour
{
    UDPReceive uDPReceive;
    string data;
    string[] points;

    public GameObject MainMenu;
    string points1;
    string points2;
    public TextMeshProUGUI textJarak;


    // Start is called before the first frame update
    void Start()
    {
        

        uDPReceive = MainMenu.GetComponent<UDPReceive>();
        data = uDPReceive.data;
    }

    // Update is called once per frame
    void Update()
    {
        DataHandle();

        int faceDistance = int.Parse(points[4]);
        int faceLost = int.Parse(points1);
    
        if(faceDistance > 75 && faceLost > 0)
        {
            textJarak.text = " Anda terlalu jauh ";
        }
        else if (faceDistance < 40 && faceLost > 0)
        {
            textJarak.text = " Anda terlalu dekat ";
        }
        else if (faceLost == 0)
        {
            textJarak.text = " Wajah Anda Hilang";
        }
        else
        {
            textJarak.text = " ";
        }


        Debug.Log(points[4]);
    }

    private void DataHandle()
    {
        data = uDPReceive.data;
        // Remove the first and last character
        data = data.Remove(0, 1);
        data = data.Remove(data.Length - 1, 1);
        points = data.Split(", ");
        points1 = points[2].Remove(0,1);
        points1 = points[3].Remove(points[3].Length - 1, 1);
        // print(points[0]+""+ points[1]); // Print the first point for debugging
    }
}

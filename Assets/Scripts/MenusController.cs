using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenusController : MonoBehaviour
{
    [SerializeField] private Button retryButton, homeButton;

    public GameObject udp;

    UDPReceive uDPReceive;
    string data;
    string[] points;

    public void Start()
    {
        uDPReceive = udp.GetComponent<UDPReceive>();
        retryButton.onClick.AddListener(OnClickRetry);
        homeButton.onClick.AddListener(OnClickHome);
    }
    public void Update()
    {
        DataHandle();

        if (points[5] == "1")
        {
            OnClickRetry();
        }
        else if(points[5] == "2")
        {
            OnClickHome();
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

    private void OnClickRetry()
    {
        Scene currentScene = SceneManager.GetActiveScene();
            
        SceneManager.LoadScene(currentScene.name);
    }
    private void OnClickHome()
    {
        SceneManager.LoadScene("MainMenu");
    }

}

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
    public bool isPauseDelay;
    public void Start()
    {    
        isPauseDelay = true;
        uDPReceive = udp.GetComponent<UDPReceive>();
        retryButton.onClick.AddListener(OnClickRetry);
        homeButton.onClick.AddListener(OnClickHome);
    }
    public void Update()
    {
        DataHandle();
        if(!isPauseDelay)
        {
            if (points[9] == "'left'")
            {
                OnClickRetry();
            }
            else if(points[9] == "'right'")
            {
                OnClickHome();
            }

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

    public void OnClickRetry()
    {
        Scene currentScene = SceneManager.GetActiveScene();
            
        SceneManager.LoadScene(currentScene.name);
    }
    public void OnClickHome()
    {
        SceneManager.LoadScene("MainMenu 1");
    }
    public IEnumerator DelayPause()
    {
        yield return new WaitForSeconds(3f);
        isPauseDelay = false;
    }
    
}

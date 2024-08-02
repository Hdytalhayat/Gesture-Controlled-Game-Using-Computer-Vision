using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    public Sprite tutorialSprite;
    public Image TutorialImg;
    public GameObject udp, tutorial;
    public bool IsTutorial;

    UDPReceive uDPReceive;
    string data;
    string[] points;
    
    bool delay = true;
    void Start()
    {
        IsTutorial = true;
        TutorialImg.sprite = tutorialSprite;
        uDPReceive = udp.GetComponent<UDPReceive>();
        StartCoroutine(DelayTutor());
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
        if(points[9] == "'enter'" && !delay)
        {
            IsTutorial =false;
            tutorial.SetActive(false);
        }
    }
    IEnumerator DelayTutor()
    {
        yield return new WaitForSeconds(3f);
        delay = false;
    }
}

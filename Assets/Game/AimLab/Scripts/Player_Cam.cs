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


    // Start is called before the first frame update

    private void Start()
    {

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

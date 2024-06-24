using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Grid_Cam_Script : MonoBehaviour
{
    float camX;
    float camY;
    float rotateCamX;
    float rotateCamY;
    public float mouseSentivity = 100f;
    public static bool  isPlayable;
    public Slider sensiSlider;
    Grid_target_spawner spawner;
    public TMP_Text clickToStart;


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
        spawner = GameObject.Find("GameHandler").GetComponent<Grid_target_spawner>();
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 0;
        isPlayable = false;

    }

    void Awake()
    {
        transform.Rotate(0, 0, 0);
        //GameObject.Find("GameHandler").GetComponent<Timer_Script>().on_Time_finished += onTimeFinished;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isPlayable)
        {
            camX += Input.GetAxis("Mouse X") * mouseSentivity * Time.deltaTime;
            camY += Input.GetAxis("Mouse Y") * mouseSentivity * Time.deltaTime;
            camY = Mathf.Clamp(camY, -90, 90);
            camX = Mathf.Clamp(camX, -90, 90);
            //transform.Rotate(rotateCamX,rotateCamY, 0); 
            //transform.localEulerAngles = new Vector3(rotateCamX, 0, 0);
            transform.localRotation = Quaternion.Euler(-camY, camX, 0);
            if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X))
            {
                shoot();
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1"))
            {
                isPlayable = true;
                Time.timeScale = 1;
                clickToStart.enabled = false;
            }
        }
    }

    void shoot()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position , transform.forward , out hit))
        {
            spawner.destroyTarget(hit.transform.gameObject);
        }
    }

    void onTimeFinished()
    {
        //timeRemaining = false;
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

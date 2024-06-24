using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Grid_Menu_Script : MonoBehaviour
{
    public GameObject PauseMenuUI;
    public GameObject OptionMenuUI;
    public GameObject aimCursor;
    bool isPause;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Grid_Cam_Script.isPlayable)
        {
            if (Input.GetKeyDown(KeyCode.Escape)&& OptionMenuUI.activeSelf==false)
            {  
                if (isPause)
                    resume();
                else
                    pause();
            }
        }
    }

    void pause()
    {
        aimCursor.SetActive(false);
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        isPause = true;
    }

     public void resume()
    {
        aimCursor.SetActive(true);
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        isPause = false;
    }

    public void mainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

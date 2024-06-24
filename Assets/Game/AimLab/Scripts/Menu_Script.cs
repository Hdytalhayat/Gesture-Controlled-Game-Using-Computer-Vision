using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Menu_Script : MonoBehaviour
{
    public static bool gameIsPaused;
    public static bool gameIsPlayable;
    public GameObject pauseMenuUI;
    public GameObject mainMenuUI;
    public GameObject gameEndMenuUI;
    public GameObject optionMenuUI;
    public TMP_Text accuracy;
    public TMP_Text avgAccTime;
    Timer_Script timer;
    Gun_Script gun;
    public TMP_Text clickToStart;
    public GameObject aimCursor;
    bool isPlayable;
    public TMP_Dropdown timeSetter;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
        GameObject.Find("GameHandler").GetComponent<Timer_Script>().on_Time_finished += onTimeFinished;
        timer = GameObject.Find("GameHandler").GetComponent<Timer_Script>();
        gun = GameObject.Find("Gun").GetComponent<Gun_Script>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !isPlayable)
        {
            Play();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (mainMenuUI.activeSelf == false && gameEndMenuUI.activeSelf == false && optionMenuUI.activeSelf == false)
            {
                if (gameIsPaused)
                {
                    resume();
                }
                else
                {
                    pause();
                }
            }
            if (optionMenuUI.activeSelf == true)
            {
                optionMenuUI.SetActive(false);
                pauseMenuUI.SetActive(true);
            }

        }
        if (Input.GetKeyDown(KeyCode.Z))
            Time.timeScale = 0;
        if (Input.GetKeyDown(KeyCode.X))
            Time.timeScale = .3f;
        if (Input.GetKeyDown(KeyCode.C))
            Time.timeScale = 1;
    }

    public void resume()
    {
        aimCursor.SetActive(true);
        gameIsPaused = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void pause()
    {
        aimCursor.SetActive(false);
        gameIsPaused = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void Play()
    {
        isPlayable = true;
        timer.time = timer.maxTime;
        gameIsPlayable = true;
        gameIsPaused = false;
        mainMenuUI.SetActive(false);
        clickToStart.enabled = false;
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void exit()
    {
        Application.Quit();
    }

    public void mainMenu()
    {
        //Gun_Script.totalBulletHitTarget = 0;
        //Gun_Script.totalBulletShoot = 0;
        //timer.time = timer.maxTime;
        //timer.displayTime(timer.time);
        SceneManager.LoadScene("MainMenu");
    }

    void onTimeFinished()
    {
        gameIsPlayable = false;
        Time.timeScale = 0;
        Debug.Log(Gun_Script.totalBulletHitTarget);
        Debug.Log(Gun_Script.totalBulletShoot);
        setAccuracy();
        gameEndMenuUI.SetActive(true);
        aimCursor.SetActive(false);
    }

    public void playAgain()
    {
        timer.time = timer.maxTime;
        Gun_Script.totalBulletHitTarget = 0;
        Gun_Script.totalBulletShoot = 0;
        timer.avgReactionTime = 0;
        timer.reactionTime.Clear();
        Time.timeScale = 1f;
        gameEndMenuUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        gameIsPlayable = true;
        aimCursor.SetActive(true);
        gameIsPaused = false;
        gun.score = 0;
        gun.text_Score.text = gun.score.ToString();
    }

    void setAccuracy()
    {
        int acc;
        if (Gun_Script.totalBulletHitTarget == 0)
        {
            acc = 0;
        }
        else
        {
            acc = (int)(Gun_Script.totalBulletHitTarget / Gun_Script.totalBulletShoot * 100);
        }
        Debug.Log(acc);
        accuracy.text = acc.ToString()+"%";
        avgAccTime.text = timer.avgReactionTime.ToString("0.00") + "s";
    }

    public void setMaxTime()
    {
        if (timeSetter.value == 0)
            timer.maxTime = 15;
        else if(timeSetter.value == 1)
            timer.maxTime = 30;
        else if(timeSetter.value == 2)
            timer.maxTime = 60;
        else if(timeSetter.value == 3)
            timer.maxTime = 120;
    }

}

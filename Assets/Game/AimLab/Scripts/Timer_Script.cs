using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Timer_Script : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text timer;
    public float time;
    public  float maxTime = 30f;
    bool timerIsRunning;
    public event Action on_Time_finished;
    float timeElapsed;
    public ArrayList reactionTime;
    public float  avgReactionTime;


    private void Awake()
    {
        displayTime(maxTime);
        time = maxTime;
        timerIsRunning = true;
        GameObject.Find("Gun").GetComponent<Gun_Script>().on_TargetHit += targetHit;
        on_Time_finished += onTimefinished;

        reactionTime = new ArrayList();
    }

    private void onTimefinished()
    {  
        Cursor.lockState = CursorLockMode.None;
        timerIsRunning = true;
        foreach(float t in reactionTime)
        {
            avgReactionTime += t;
        }
        avgReactionTime = avgReactionTime / reactionTime.Count;
    }

    // Update is called once per frame
    void Update()
    {
        if (Menu_Script.gameIsPlayable)
        {
            if (timerIsRunning)
            {
                if (time > 0)
                {
                    time -= Time.deltaTime;
                    displayTime(time);
                    timeElapsed += Time.deltaTime;
                }
                else
                {
                    time = 0;
                    displayTime(time);
                    Debug.Log("time out");
                    timerIsRunning = false;
                    on_Time_finished?.Invoke();
                }
            }
        }
        
    }

    public void displayTime(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void targetHit(object sender, System.EventArgs e)
    {
        reactionTime.Add(timeElapsed);
        timeElapsed = 0;
    }
    
}

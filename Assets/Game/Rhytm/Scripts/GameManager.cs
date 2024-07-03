using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GManager : MonoBehaviour
{
    public AudioSource music;
    public bool start;
    public BeatScroller scroller;
    public static GManager instance;
    public float currentScore;
    public float perfectScore = 0.02f;
    public float goodScore = 0.01f;
    public float missed = 0.01f;

    public TextMeshProUGUI perfectText;
    public TextMeshProUGUI goodText;
    public TextMeshProUGUI missText;

    //public TextMeshProUGUI scoreText;
    public bool gameIsRunning;
    public Image HP;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        //scoreText.text = "Score : 0";
        gameIsRunning = true;

        StartCoroutine(Timer());
    }

    // Update is called once per frame
    void Update()
    {
        if (!start)
        {
            if (Input.anyKeyDown)
            {
                start = true;
                scroller.hasStarted = true;

                music.Play();
            }
        }

        if (currentScore < 0 && gameIsRunning)
        {
            /*StopTime();*/
        }
    }

    public void PerfectHit()
    {
        Debug.Log("Perfect");


        currentScore += perfectScore;
        HP.fillAmount += perfectScore;

        StartCoroutine(ShowPopUp(perfectText));
    }

    public void GoodHit()
    {
        Debug.Log("Good");

        currentScore += goodScore;
        HP.fillAmount += goodScore;

        StartCoroutine(ShowPopUp(goodText));
    }

    public void NoteMiss()
    {
        Debug.Log("Missed");

        currentScore -= missed;
        HP.fillAmount -= missed;

        StartCoroutine(ShowPopUp(missText));
    }

    //public void StopTime()
    //{
    //    Debug.Log("Game has stopped due to score less than 0");

    //    gameIsRunning = false;
    //    music.Pause();
    //    Time.timeScale = 0;
    //}

    public IEnumerator Timer()
    {
        while (true)
        {
            HP.fillAmount -= missed;
            yield return new WaitForSeconds(1f);
        }
    }

    private IEnumerator ShowPopUp(TextMeshProUGUI textElement)
    {
        textElement.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        textElement.gameObject.SetActive(false);
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class AlphabetGame2 : MonoBehaviour
{
    public Text letterText; // UI Text to display the current letter
    public Text scoreText; // UI Text to display the score
    public Text spawnIntervalText;
    public TextMeshProUGUI resultText; // UI Text to display the final score
    public TextMeshProUGUI highScoreText; // UI Text to display the high score
    public TextMeshProUGUI letterRateText; // UI Text to display the letter rate

    private char currentLetter;
    private int score = 0;
    private int highScore = 0;
    private int correctLetters = 0;
    private bool canScore; // Flag to indicate if scoring is allowed
    public GameObject udphandler;

    float wait = 1f;
    UDPReceive uDPReceive;
    string data;
    string[] points;
    char CharInput;

    //Pause Menu
    [SerializeField] private GameObject pauseMenu;
    private bool isPauseActive;

    void Start()
    {
        uDPReceive = udphandler.GetComponent<UDPReceive>();
        data = uDPReceive.data;

        score = 0;
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateScore();
        UpdateHighScore();
        StartCoroutine(InitialDelay());
        isPauseActive = false;
    }

    IEnumerator InitialDelay()
    {
        yield return new WaitForSeconds(5f); // Delay 5 detik pada awal mulai
        StartCoroutine(Countdown());
        StartCoroutine(SpawnLetter());
    }

    IEnumerator Countdown()
    {
        spawnIntervalText.gameObject.SetActive(true);
        for (int i = 120; i > 0; i--)
        {
            spawnIntervalText.text = "Time: " + i;
            yield return new WaitForSeconds(1f);
        }
        spawnIntervalText.gameObject.SetActive(false);
        resultText.gameObject.SetActive(true);
        resultText.text = "Score Akhir: " + score;
        letterRateText.gameObject.SetActive(true);
        letterRateText.text = "Letter/second: " + (correctLetters / 120f).ToString("F2");
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    IEnumerator SpawnLetter()
    {
        while(true)
        {
            canScore = true; // Reset scoring ability for the new letter
            currentLetter = (char)('A' + Random.Range(0, 26));
            letterText.text = currentLetter.ToString();
            letterText.color = Color.white;
            yield return new WaitUntil(() => !canScore);
        }
    }

    void OnLetterKeyPressed(char letter)
    {
        if (canScore)
        {
            if (letter == currentLetter)
            {
                score++;
                correctLetters++;
                letterText.color = Color.green;
                UpdateScore();
                canScore = false; // Disable scoring until the next letter is spawned
            }
        }
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    void UpdateHighScore()
    {
        highScoreText.text = "High Score: " + highScore;
    }

    void HighScore()
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            UpdateHighScore();
        }
    }

    void OnDestroy()
    {
        HighScore();
    }
}
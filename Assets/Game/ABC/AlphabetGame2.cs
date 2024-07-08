using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AlphabetGame2 : MonoBehaviour
{
    public Text letterText; // UI Text to display the current letter
    public float spawnInterval = 2f; // Time between letter spawns
    public Text scoreText; // UI Text to display the score
    public Text spawnIntervalText;

    private char currentLetter;
    private int score = 0;
    private bool canScore; // Flag to indicate if scoring is allowed
    public GameObject udphandler;

    float wait = 1f;
    UDPReceive uDPReceive;
    string data;
    string[] points;
    char CharInput;

    private void DataHandle()
    {
        data = uDPReceive.data;
        // Remove the first and last character
        data = data.Remove(0, 1);
        data = data.Remove(data.Length - 1, 1);
        points = data.Split(", ");
        CharInput = points[0][1];
        // print(points[0]+""+ points[1]); // Print the first point for debugging
    }
    void Start()
    {
        uDPReceive = udphandler.GetComponent<UDPReceive>();
        data = uDPReceive.data;

        score = 0;
        UpdateScore();
        StartCoroutine(SpawnLetter());
    }

    void Update()
    {
        DataHandle();
        Debug.Log(CharInput+""+currentLetter);
        OnLetterKeyPressed(char.ToUpper(CharInput));
        
    }

    IEnumerator SpawnLetter()
    {
        while(true)
        {
            spawnIntervalText.gameObject.SetActive(true);
            spawnIntervalText.text = "Timer = 10";
            yield return new WaitForSeconds(wait);
            spawnIntervalText.text = "Timer = 9";
            yield return new WaitForSeconds(wait);
            spawnIntervalText.text = "Timer = 8";
            yield return new WaitForSeconds(wait);
            spawnIntervalText.text = "Timer = 7";
            yield return new WaitForSeconds(wait);
            spawnIntervalText.text = "Timer = 6";
            yield return new WaitForSeconds(wait);
            spawnIntervalText.text = "Timer = 5";
            yield return new WaitForSeconds(wait);
            spawnIntervalText.text = "Timer = 4";
            yield return new WaitForSeconds(wait);
            spawnIntervalText.text = "Timer = 3";
            yield return new WaitForSeconds(wait);
            spawnIntervalText.text = "Timer = 2";
            yield return new WaitForSeconds(wait);
            spawnIntervalText.text = "Timer = 1";
            yield return new WaitForSeconds(wait);
            spawnIntervalText.gameObject.SetActive(false);
            

            canScore = true; // Reset scoring ability for the new letter
            currentLetter = (char)('A' + Random.Range(0, 26));
            letterText.text = currentLetter.ToString();
            letterText.color = Color.white;
        }


    }

    void OnLetterKeyPressed(char letter)
    {
        if (canScore)
        {
            if (letter == currentLetter)
            {
                score++;
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
}

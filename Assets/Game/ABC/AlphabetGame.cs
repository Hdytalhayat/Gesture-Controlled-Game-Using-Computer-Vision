using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AlphabetGame : MonoBehaviour
{
    public Text[] letterTexts; // Array of UI Text elements to display the alphabets
    public float spawnInterval = 2f; // Time between letter spawns
    public Text scoreText; // UI Text to display the score
    public Text spawnIntervalText; // UI Text to display the spawn interval
    

    private char[] currentLetters = new char[2];
    private bool[] letterCorrect = new bool[2]; // Track if each letter has been correctly identified
    private int score;
    private bool canScore; // Flag to indicate if scoring is allowed
    private bool incorrectPressed; // Flag to track if incorrect key has been pressed

    void Start()
    {
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnLetters());
    }

    void Update()
    {
        if (canScore)
        {
            foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keyCode))
                {
                    char pressedLetter = System.Char.ToUpper((char)keyCode);

                    bool correctPressed = false;

                    for (int i = 0; i < 2; i++)
                    {
                        if (pressedLetter == currentLetters[i])
                        {
                            if (!letterCorrect[i])
                            {
                                letterCorrect[i] = true;
                                letterTexts[i].color = Color.green; // Change color to green on correct
                                correctPressed = true;
                            }
                        }
                    }

                    if (!correctPressed && !incorrectPressed)
                    {
                        // Incorrect letter pressed
                        score--; // Decrease score if any letter is incorrect
                        UpdateScore();
                        StartCoroutine(ShowIncorrectFeedback());
                        incorrectPressed = true; // Set flag to true
                    }

                    // Check if both letters have been correctly identified
                    if (letterCorrect[0] && letterCorrect[1])
                    {
                        score += 3; // Increase score when both letters are correct
                        UpdateScore();
                        canScore = false; // Disable scoring until the next letters are spawned
                        StartCoroutine(ClearTextColors());
                    }
                }
            }
        }
        /*UpdateSpawnIntervalText();*/
    }

    IEnumerator SpawnLetters()
    {
        while (true)
        {
            spawnIntervalText.gameObject.SetActive(true);
            spawnIntervalText.text = "Count = 10";
            yield return new WaitForSeconds(1f);
            spawnIntervalText.text = "Count = 9";
            yield return new WaitForSeconds(1f);
            spawnIntervalText.text = "Count = 8";
            yield return new WaitForSeconds(1f);
            spawnIntervalText.text = "Count = 7";
            yield return new WaitForSeconds(1f);
            spawnIntervalText.text = "Count = 6";
            yield return new WaitForSeconds(1f);
            spawnIntervalText.text = "Count = 5";
            yield return new WaitForSeconds(1f);
            spawnIntervalText.text = "Count = 4";
            yield return new WaitForSeconds(1f);
            spawnIntervalText.text = "Count = 3";
            yield return new WaitForSeconds(1f);
            spawnIntervalText.text = "Count = 2";
            yield return new WaitForSeconds(1f);
            spawnIntervalText.text = "Count = 1";
            yield return new WaitForSeconds(1f);
            spawnIntervalText.gameObject.SetActive(false);

            currentLetters[0] = (char)('A' + Random.Range(0, 26));
            currentLetters[1] = (char)('A' + Random.Range(0, 26));

            // Reset letter correct status
            letterCorrect[0] = false;
            letterCorrect[1] = false;
            incorrectPressed = false; // Reset incorrect pressed flag

            // Display the letters on UI
            letterTexts[0].text = currentLetters[0].ToString();
            letterTexts[1].text = currentLetters[1].ToString();

            canScore = true; // Enable scoring for the new letters
        }
    }

    IEnumerator ShowIncorrectFeedback()
    {
        // Change color to red on incorrect
        for (int i = 0; i < 2; i++)
        {
            if (!letterCorrect[i] && letterTexts[i] != null)
            {
                letterTexts[i].color = Color.red;
            }
        }

        yield return new WaitForSeconds(1f); // Display correct/incorrect color for 1 second

        // Reset text colors
        for (int i = 0; i < 2; i++)
        {
            if (letterTexts[i] != null)
            {
                letterTexts[i].color = Color.white;
            }
        }
    }

    IEnumerator ClearTextColors()
    {
        yield return new WaitForSeconds(1f); // Display correct/incorrect color for 1 second

        // Reset text colors
        for (int i = 0; i < 2; i++)
        {
            if (letterTexts[i] != null)
            {
                letterTexts[i].color = Color.white;
            }
        }
    }

    /*void UpdateSpawnIntervalText()
    {
        spawnIntervalText.text = "Spawn Interval: " + spawnInterval;
    }*/

    void UpdateScore()
    {
        scoreText.text = "Nilai: " + score;
    }
}

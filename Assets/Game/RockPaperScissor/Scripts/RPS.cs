using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RPS : MonoBehaviour
{
    // Enum for the three possible choices
    public enum Choice { Rock, Paper, Scissor }

    // UI Image to display the current choice
    public Image choiceImage;

    // UI Image to display the player's choice
    public Image playerImage;

    // Images for rock, paper, and scissor
    public Sprite[] rps;

    // UI Text to display the result, countdown, and score
    public TextMeshProUGUI resultText;
    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI scoreText;

    // Current choice
    private Choice currentChoice;

    // Timer to spawn new choices every 5 seconds
    private float timer = 5f;

    // Input keys
    private KeyCode rockInput = KeyCode.A;
    private KeyCode paperInput = KeyCode.B;
    private KeyCode scissorInput = KeyCode.C;

    // Flag to check if input has been received
    private bool inputReceived = false;

    // Score
    private int score = 0;

    void Start()
    {
        StartRound();
        UpdateScoreText();
    }

    void Update()
    {
        // Countdown timer


        timer -= Time.deltaTime;
        countdownText.text = Mathf.Ceil(timer).ToString();

        if (timer <= 0f)
        {
            // If time runs out and no input received
            countdownText.text = "0";
            Invoke("ChangeImage", 3f);
            inputReceived = true;
            currentChoice = GetRandomChoice();
        
            if (Input.GetKeyDown(rockInput))
            {
                playerImage.sprite = rps[0];
                CheckChoice(Choice.Rock);
            }
            else if (Input.GetKeyDown(paperInput))
            {
                playerImage.sprite = rps[1];
                CheckChoice(Choice.Paper);
            }
            else if (Input.GetKeyDown(scissorInput))
            {
                playerImage.sprite = rps[2];
                CheckChoice(Choice.Scissor);
            }
        }
        else{
            choiceImage.sprite = rps[Random.Range(0,rps.Length)];

        }
        
    }

    // Get a random choice
    private Choice GetRandomChoice()
    {
        int randomIndex = Random.Range(0, 3);
        return (Choice)randomIndex;
    }

    // Update the UI image to display the current choice
    private void UpdateChoiceImage()
    {
        switch (currentChoice)
        {
            case Choice.Rock:
                choiceImage.sprite = rps[0];
                break;
            case Choice.Paper:
                choiceImage.sprite = rps[1];
                break;
            case Choice.Scissor:
                choiceImage.sprite = rps[2];
                break;
        }
    }

    // Check the player's choice and update the result
    private void CheckChoice(Choice playerChoice)
    {
        if (playerChoice == currentChoice)
        {
            resultText.text = "Correct!";
            score += 1;
        }
        else
        {
            resultText.text = "Wrong!";
        }
        inputReceived = true;
        UpdateScoreText();
        Invoke("ChangeImage", 3f);
    }

    // Change the image after 3 seconds
    private void ChangeImage()
    {
        StartRound();
    }

    // Start a new round
    private void StartRound()
    {
        
        UpdateChoiceImage();
        timer = 5f;
        countdownText.text = "5";
        resultText.text = "";
        inputReceived = true;
        playerImage.sprite = null;
    }

    // Update the score text
    private void UpdateScoreText()
    {
        scoreText.text = "Score = " + score;
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RPS : MonoBehaviour
{
    public enum Choice
    {
        Rock = 0,
        Paper = 1,
        Scissors = 2
    }
    Choice playerChoice, opponentChoice;
    // UI Image to display the current choice
    public Image comImg;
    // UI Image to display the player's choice
    public Image playerImage;
    // Images for rock, paper, and scissor
    public Sprite[] rps;

    private int player, com;

    // UI Text to display the result, countdown, and score
    public TextMeshProUGUI resultText, countdownText, playerScoreText, comScoreText;

    // Timer to spawn new choices every 5 seconds
    private float timer = 5f;
    // Score
    private int playerScore = 0;
    private int comScore = 0;
    private bool canScore = false;
    private bool inputed = false;


    UDPReceive uDPReceive;
    string data;
    string[] points;

    private void DataHandle()
    {
        data = uDPReceive.data;
        // Remove the first and last character
        data = data.Remove(0, 1);
        data = data.Remove(data.Length - 1, 1);
        points = data.Split(", ");
        // print(points[0]+""+ points[1]); // Print the first point for debugging
    }
    void Start()
    {
        uDPReceive = GetComponent<UDPReceive>();
        com = Random.Range(0, 3);
        if (com == 0)
        {
            opponentChoice = Choice.Rock;
        }
        else if (com == 1)
        {
            opponentChoice = Choice.Paper;
        }
        else if (com == 2)
        {
            opponentChoice = Choice.Scissors;
        }
        playerScoreText.text = playerScore.ToString();
        comScoreText.text = comScore.ToString();
    }

    void Update()
    {
        DataHandle();
        Debug.Log(points[5]);
        comImg.sprite = rps[Random.Range(0, 3)];
        // Countdown timer
        timer -= Time.deltaTime;
        countdownText.text = Mathf.Ceil(timer).ToString();
        if (timer <= 0)
        {
            countdownText.text = "0";
            canScore = true;
            comImg.sprite = rps[com];
            if (canScore && !inputed)
            {
                if (points[5]=="'Rock'")
                {
                    player = 0;
                    playerChoice = Choice.Rock;
                    playerImage.sprite = rps[0];
                    inputed = true;
                    DetermineWinner(playerChoice, opponentChoice);
                }
                else if (points[5]=="'Paper'")
                {
                    player = 1;
                    playerChoice = Choice.Paper;
                    playerImage.sprite = rps[1];
                    inputed = true;
                    DetermineWinner(playerChoice, opponentChoice);

                }
                else if (points[5]=="'Scissor'")
                {
                    player = 2;
                    playerChoice = Choice.Scissors;
                    playerImage.sprite = rps[2];
                    inputed = true;
                    DetermineWinner(playerChoice, opponentChoice);
                }
                Invoke("StartImage", 1f);
            }
        }
        else
        {
            resultText.text = "Wait";
        }
    }
    void StartImage()
    {
        timer = 5f;
        com = Random.Range(0, 3);
        if (com == 0)
        {
            opponentChoice = Choice.Rock;
        }
        else if (com == 1)
        {
            opponentChoice = Choice.Paper;
        }
        else if (com == 2)
        {
            opponentChoice = Choice.Scissors;
        }
        canScore = false;
        if (timer <= 0)
        {
            resultText.text = "Now";
        }
        inputed = false;
    }
    public void DetermineWinner(Choice playerChoice, Choice opponentChoice)
    {
        if (playerChoice == opponentChoice)
        {
            resultText.text = "Draw!";
        }
        else if ((playerChoice == Choice.Rock && opponentChoice == Choice.Scissors) ||
                 (playerChoice == Choice.Paper && opponentChoice == Choice.Rock) ||
                 (playerChoice == Choice.Scissors && opponentChoice == Choice.Paper))
        {
            playerScore += 1;
            resultText.text = "Player wins!";
        }
        else
        {
            comScore += 1;
            resultText.text = "Opponent wins!";
        }
        playerScoreText.text = playerScore.ToString();
        comScoreText.text = comScore.ToString();
    }
}

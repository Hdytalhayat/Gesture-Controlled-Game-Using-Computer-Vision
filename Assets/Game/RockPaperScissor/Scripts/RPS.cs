using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RPS : MonoBehaviour
{

    // UI Image to display the current choice
    public Image comImg;
    // UI Image to display the player's choice
    public Image playerImage;
    // Images for rock, paper, and scissor
    public Sprite[] rps;

    private int player, com;

    // UI Text to display the result, countdown, and score
    public TextMeshProUGUI resultText;
    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI scoreText;

    // Timer to spawn new choices every 5 seconds
    private float timer = 5f;
    // Score
    private int score = 0;
    private bool canScore = false;
    private bool inputed = false;

    void Start()
    {
        com = Random.Range(0,3);
        scoreText.text = score.ToString();
    }

    void Update()
    {
       
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
                if (Input.GetKeyDown(KeyCode.A))
                {
                    player = 0;
                    Check(player);
                    playerImage.sprite = rps[0];
                    inputed = true;
                }
                else if (Input.GetKeyDown(KeyCode.B))
                {
                    player = 1;
                    Check(player);
                    playerImage.sprite = rps[1];
                    inputed = true;
                }
                else if (Input.GetKeyDown(KeyCode.C))
                {
                    player = 2;
                    Check(player);
                    playerImage.sprite = rps[2];
                    inputed = true;
                }
                Invoke("StartImage", 3f);
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
        canScore = false;
        if (timer <= 0){
            resultText.text = "Now";
        }
        inputed = false;
    }
    void Check(int p)
    {
        if(p == com)
        {
            score +=1;
            resultText.text = "Correct";
        }
        else
        {
            resultText.text = "wrong";
        }
        scoreText.text = score.ToString();
    }
}

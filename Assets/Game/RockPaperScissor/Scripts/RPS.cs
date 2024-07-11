using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RPS : MonoBehaviour
{
// // Enum for the three possible choices
    public enum Choice { Rock, Paper, Scissor }

    // UI Image to display the current choice
    public Image choiceImage;

    // Images for rock, paper, and scissor
    public Sprite rockImage;
    public Sprite paperImage;
    public Sprite scissorImage;

    // UI Text to display the result
    public TextMeshProUGUI resultText;

    // Current choice
    private Choice currentChoice;

    // Timer to spawn new choices every 5 seconds
    private float timer = 0f;

    // Input keys
    private KeyCode rockInput = KeyCode.A;
    private KeyCode paperInput = KeyCode.B;
    private KeyCode scissorInput = KeyCode.C;
    // Flag to check if input has been received
    private bool inputReceived = false;

    void Start()
    {
        // Initialize the current choice to a random value
        currentChoice = GetRandomChoice();
        UpdateChoiceImage();
    }

    void Update()
    {
        // Increment the timer
        timer += Time.deltaTime;

        // If the timer reaches 5 seconds, spawn a new choice
        if (timer >= 5f && !inputReceived)
        {
            timer = 0f;
            currentChoice = GetRandomChoice();
            UpdateChoiceImage();
            inputReceived = false;
        }

        // Check for input
        if (Input.GetKeyDown(rockInput) && currentChoice == Choice.Rock)
        {
            resultText.text = "Correct!";
            inputReceived = true;
            Invoke("ChangeImage", 3f);
        }
        else if (Input.GetKeyDown(paperInput) && currentChoice == Choice.Paper)
        {
            resultText.text = "Correct!";
            inputReceived = true;
            Invoke("ChangeImage", 3f);
        }
        else if (Input.GetKeyDown(scissorInput) && currentChoice == Choice.Scissor)
        {
            resultText.text = "Correct!";
            inputReceived = true;
            Invoke("ChangeImage", 3f);
        }
        else if (Input.GetKeyDown(rockInput) || Input.GetKeyDown(paperInput) || Input.GetKeyDown(scissorInput))
        {
            resultText.text = "Wrong!";
            inputReceived = true;
            Invoke("ChangeImage", 3f);
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
                choiceImage.sprite = rockImage;
                break;
            case Choice.Paper:
                choiceImage.sprite = paperImage;
                break;
            case Choice.Scissor:
                choiceImage.sprite = scissorImage;
                break;
        }
    }

    // Change the image after 3 seconds
    private void ChangeImage()
    {
        currentChoice = GetRandomChoice();
        UpdateChoiceImage();
        timer = 0f;
        inputReceived = false;
    }
}

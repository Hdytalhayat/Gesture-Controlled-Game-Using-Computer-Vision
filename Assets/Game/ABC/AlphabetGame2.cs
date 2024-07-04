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
    private int score;
    private bool canScore; // Flag to indicate if scoring is allowed

    void Start()
    {
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnLetter());
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            foreach (char c in Input.inputString)
            {
                if (char.IsLetter(c))
                {
                    OnLetterKeyPressed(char.ToUpper(c));
                }
            }
        }
    }

    IEnumerator SpawnLetter()
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

            currentLetter = (char)('A' + Random.Range(0, 26));
            letterText.text = currentLetter.ToString();
            canScore = true; // Reset scoring ability for the new letter

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
            }
            else
            {
                score--;
                letterText.color = Color.red;
            }

            UpdateScore();
            canScore = false; // Disable scoring until the next letter is spawned
        }
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using System.Collections;
using TMPro;

[System.Serializable]
public class Question
{
    public string question;
    public string[] options;
    public int correctAnswer;
}

[System.Serializable]
public class QuizData
{
    public List<Question> questions;
}

public class QuizManagerScript : MonoBehaviour
{
    public GameObject menus;
    public TextMeshProUGUI questionText;
    public Button[] optionButtons;
    public TextMeshProUGUI resultText;
    public TextMeshProUGUI scoreText;

    private QuizData quizData;
    private int currentQuestionIndex = 0;
    private int score = 0;
    private bool canAnswer = true;  // Tambahkan ini

    public AudioSource audioSource;
    public AudioClip correctSound; 
    public AudioClip wrongSound;

    public GameObject udp;
    UDPReceive uDPReceive;
    string data;
    string[] points;

    void Start()
    {
        menus.SetActive(false);
        uDPReceive = udp.GetComponent<UDPReceive>();
        data = uDPReceive.data;
        LoadQuizData();
        ShowQuestion();
        UpdateScoreDisplay();
    }

    void LoadQuizData()
    {
        string jsonFilePath = Path.Combine(Application.streamingAssetsPath, "quiz_questions.json");
        string jsonContent = File.ReadAllText(jsonFilePath);
        quizData = JsonUtility.FromJson<QuizData>(jsonContent);
    }

    void ShowQuestion()
    {
        if (currentQuestionIndex < quizData.questions.Count)
        {
            Question currentQuestion = quizData.questions[currentQuestionIndex];
            questionText.text = currentQuestion.question;

            for (int i = 0; i < optionButtons.Length; i++)
            {
                optionButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.options[i];
                int optionIndex = i;
                optionButtons[i].onClick.RemoveAllListeners();
                optionButtons[i].onClick.AddListener(() => CheckAnswer(optionIndex));
            }

            resultText.text = "";
            canAnswer = true;  // Tambahkan ini
            EnableButtons(true);  // Tambahkan ini
        }
        else
        {
            Invoke("ShowMenu", 2f);
            questionText.text = "Quiz selesai!";
            resultText.text = "Skor akhir Anda: " + score + " dari " + quizData.questions.Count;
            EnableButtons(false);  // Ganti ini
        }
    }

    void CheckAnswer(int selectedAnswer)
    {
        if (!canAnswer) return;  // Tambahkan ini
        canAnswer = false;  // Tambahkan ini
        EnableButtons(false);  // Tambahkan ini

        Question currentQuestion = quizData.questions[currentQuestionIndex];
        if (selectedAnswer == currentQuestion.correctAnswer)
        {
            resultText.text = "Benar!";
            score++;
            audioSource.PlayOneShot(correctSound);
        }
        else
        {
            resultText.text = "Salah. Jawaban yang benar: " + currentQuestion.options[currentQuestion.correctAnswer];
            audioSource.PlayOneShot(wrongSound);
        }

        UpdateScoreDisplay();
        currentQuestionIndex++;
        StartCoroutine(NextQuestionWithDelay());  // Ganti ini
    }

    void UpdateScoreDisplay()
    {
        scoreText.text = "Skor: " + score + " / " + quizData.questions.Count;
    }

    // Tambahkan metode ini
    void EnableButtons(bool enable)
    {
        foreach (Button button in optionButtons)
        {
            button.interactable = enable;
        }
    }

    // Tambahkan metode ini
    IEnumerator NextQuestionWithDelay()
    {
        yield return new WaitForSeconds(2f);
        ShowQuestion();
    }
    void Update()
    {
        DataHandle();

        if(points[9] == "'left'" || Input.GetKeyDown(KeyCode.A))
        {
            optionButtons[0].onClick.Invoke();
        }
        else if(points[9] == "'right'" || Input.GetKeyDown(KeyCode.D))
        {
            optionButtons[1].onClick.Invoke();
        }
    }
    private void DataHandle()
    {
        data = uDPReceive.data;
        // Remove the first and last character
        data = data.Remove(0, 1);
        data = data.Remove(data.Length - 1, 1);
        points = data.Split(", ");
        // print(points[0]+""+ points[1]); // Print the first point for debugging
    }
    void ShowMenu()
    {
        menus.SetActive(true);
    }
}
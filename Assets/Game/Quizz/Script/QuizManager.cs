using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class QuestionList
{
    public List<QuestionAns> QnA;
}

public class QuizManager : MonoBehaviour
{
    public List<QuestionAns> QnA;
    public GameObject[] option;
    public TextMeshProUGUI QuestionTxt;
    public TextMeshProUGUI ScoreText;
    public GameObject QuizPanel;
    public GameObject RetryPanel;

    public AudioSource audioSource;
    public AudioClip correctSound; 
    public AudioClip wrongSound;

    private int totalQuestion;
    private int score;
    private int currentQuestionIndex;

    void Start()
    {
        RetryPanel.SetActive(false);
        LoadQuestions();
        GenerateQuestion();
    }

    void LoadQuestions()
    {
        TextAsset json = Resources.Load<TextAsset>("questions");
        if (json != null)
        {
            QuestionList questionList = JsonUtility.FromJson<QuestionList>(json.ToString());
            if (questionList != null && questionList.QnA != null)
            {
                QnA = new List<QuestionAns>(questionList.QnA); // Copy list to prevent accidental changes
                totalQuestion = QnA.Count;
            }
            else
            {
                Debug.LogError("Failed to parse JSON or QuestionList is null.");
            }
        }
        else
        {
            Debug.LogError("Failed to load questions JSON file.");
        }
    }

    void SetAnswers()
{
    // Pastikan jumlah opsi sesuai dengan jumlah jawaban yang ada
    if (option.Length != QnA[currentQuestionIndex].Answer.Length)
    {
        Debug.LogError("Jumlah opsi tidak sesuai dengan jumlah jawaban.");
        return;
    }

    // Atur jawaban untuk setiap tombol opsi
    for (int i = 0; i < option.Length; i++)
    {
        var answerScript = option[i].GetComponent<AnswerScript>();
        // Set isCorrect berdasarkan indeks 1-based
        answerScript.isCorrect = (i + 1 == QnA[currentQuestionIndex].CorrectAnswer);
        // Set teks jawaban pada tombol
        option[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = QnA[currentQuestionIndex].Answer[i];
    }
}


    void GenerateQuestion()
    {
        if (QnA.Count == 0)
        {
            GameOver();
            return;
        }

        currentQuestionIndex = Random.Range(0, QnA.Count);
        QuestionTxt.text = QnA[currentQuestionIndex].Question;
        SetAnswers();
    }

    public void Correct()
    {
        score++;
        Debug.Log($"Correct! Score: {score}");
        audioSource.PlayOneShot(correctSound);
        QnA.RemoveAt(currentQuestionIndex);
        GenerateQuestion();
    }

    public void Wrong()
    {
        Debug.Log("Wrong answer.");
        audioSource.PlayOneShot(wrongSound);
        QnA.RemoveAt(currentQuestionIndex);
        GenerateQuestion();
    }

    void GameOver()
    {
        QuizPanel.SetActive(false);
        RetryPanel.SetActive(true);
        ScoreText.text = $"{score}/{totalQuestion}";
        Debug.Log($"Game Over. Final Score: {score}/{totalQuestion}");
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
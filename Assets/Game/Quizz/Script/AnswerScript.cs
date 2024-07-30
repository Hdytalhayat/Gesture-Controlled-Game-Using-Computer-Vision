using UnityEngine;

public class AnswerScript : MonoBehaviour
{
    public bool isCorrect;
    private QuizManager quizManager;

    void OnEnable()
    {
        if (quizManager == null)
        {
            quizManager = FindObjectOfType<QuizManager>();
        }
    }

    public void Answer()
    {
        if (isCorrect)
        {
            Debug.Log("Benar");
            quizManager.Correct();
        }
        else
        {
            Debug.Log("Salah");
            quizManager.Wrong();
        }
    }
}
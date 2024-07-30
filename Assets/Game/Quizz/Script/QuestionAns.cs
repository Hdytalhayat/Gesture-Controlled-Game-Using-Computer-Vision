[System.Serializable]
public class QuestionAns
{
    public string Question;
    public string[] Answer; // Ensure this has exactly 2 elements.
    public int CorrectAnswer; // This should be 1 or 2, corresponding to the correct index in Answer array.
}
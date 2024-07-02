using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Button dynoBtn, flappyBtn, aimlabBtn;
    // Start is called before the first frame update
    private void Start()
    {
        dynoBtn.onClick.AddListener(OnClickDyno);
        flappyBtn.onClick.AddListener(OnClickFlappy);
        aimlabBtn.onClick.AddListener(OnClickAimlab);
    }

    private void OnClickDyno()
    {
        SceneManager.LoadScene("Game");
    }
    private void OnClickFlappy()
    {
        SceneManager.LoadScene("mainGame");
    }
    private void OnClickAimlab()
    {
        SceneManager.LoadScene("NormalMode");
    }
}
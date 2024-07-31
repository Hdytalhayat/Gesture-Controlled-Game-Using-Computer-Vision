using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManagerHUD : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI textOpening;
    [SerializeField]private Animator animator;
    [SerializeField]private float transitionTime = 1f;
    [SerializeField]private int indexs = 0;
    [SerializeField]private string[] textOpenings = {"Welcome", "DLI Present", "NAMA GAME"};
    [SerializeField]private int counter = 0;
    private void Start()
    {
        animator = textOpening.gameObject.GetComponent<Animator>();
        textOpening.text = textOpenings[indexs];
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && counter == 0)
        {
            counter +=1;
            StartCoroutine(PlayFade());
        }
    }
    IEnumerator PlayFade()
    {
        indexs++;
        animator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(transitionTime);
        animator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(transitionTime);
        if(indexs < textOpenings.Length)
        {
            textOpening.text = textOpenings[indexs];
        }
        else
        {
            SceneManager.LoadScene("MainMenuUtama");
        }
        counter = 0;
    }
}

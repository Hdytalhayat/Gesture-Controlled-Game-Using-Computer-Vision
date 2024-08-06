using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AboutUs : MonoBehaviour
{
    public Animator animator;
    public GameObject bg, logoUM, logoDI, logoI;
    public TextMeshProUGUI title, subtitle;
    public string[] text = { "Director", "Programmer", "Technical Artist and UI Designer", "Harits Ar Rosyid, S.T., M.T., Ph.D.", "Hidayatul Hayat\nHitatama Anindyajati Siddhi\nAnindito Setyawan", "Haffas Zikri Ariyandi" };
    private void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(playTransition());
        bg.SetActive(false);
        logoUM.SetActive(false);
        logoDI.SetActive(false);
        logoI.SetActive(false);
    }
    IEnumerator playTransition()
    {
        animator.SetTrigger("FadeIn");
        title.text = text[0];
        subtitle.text = text[3];
        yield return new WaitForSeconds(3f);
        animator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(3f);
        animator.SetTrigger("FadeIn");
        title.text = text[1];
        subtitle.text = text[4];
        yield return new WaitForSeconds(5f);
        animator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(3f);

        animator.SetTrigger("FadeIn");
        title.text = text[2];
        subtitle.text = text[5];
        yield return new WaitForSeconds(3f);
        animator.SetTrigger("FadeOut");
        title.gameObject.SetActive(false);
        subtitle.gameObject.SetActive(false);
        bg.SetActive(true);
        logoUM.SetActive(true);
        logoDI.SetActive(true);
        logoI.SetActive(true);
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(0);

    }
}

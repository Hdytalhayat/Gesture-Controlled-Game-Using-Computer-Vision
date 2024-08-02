using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuGM : MonoBehaviour
{
    public Player1Controller player1Controller;
    public Player2Controller player2Controller;
    public GameObject pauseMenu;
    public TextMeshProUGUI winnerText;
    
    void Update()
    {
        if(player1Controller.isDead)
        {
            StartCoroutine(ShowPause());
            StartCoroutine(pauseMenu.GetComponent<MenusController>().DelayPause());
            winnerText.text= "Pemain 2 Menang!";
        }
        else if(player2Controller.isDead)
        {
            StartCoroutine(ShowPause());
            StartCoroutine(pauseMenu.GetComponent<MenusController>().DelayPause());

            winnerText.text= "Pemain 1 Menang!";

        }     
    }
    IEnumerator ShowPause()
    {
        yield return new WaitForSeconds(2f);
        player1Controller.isPauseActive = true;
        pauseMenu.SetActive(true);

    }

}

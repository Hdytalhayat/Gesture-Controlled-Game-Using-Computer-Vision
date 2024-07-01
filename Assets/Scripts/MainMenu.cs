using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public Button buttonDyno;
    public Button buttonFlappy;
    public Button buttonAim;
    // Start is called before the first frame update
    void Start()
    {
        buttonDyno.onClick.AddListener(DynoS);
        buttonFlappy.onClick.AddListener(Flappy);
        buttonAim.onClick.AddListener(Aims);
        buttonDyno.Select();
    }

    private void DynoS()
    {
        SceneManager.LoadScene("Game");
    }

    private void Flappy()
    {
        SceneManager.LoadScene("mainGame");
    }

    private void Aims()
    {
        SceneManager.LoadScene("NormalMode");
    }

    private void Quit()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Z) )
        {
            DynoS();
        }

        if(Input.GetKey(KeyCode.X))
        {
            Flappy();
        }
        
        if(Input.GetKey(KeyCode.C))
        {
            Aims(); 
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    public void normalMode()
    {
        SceneManager.LoadScene("NormalMode");
    }

    public void gridShot()
    {
        SceneManager.LoadScene("GridShot");

    }

    public void exit()
    {
        Application.Quit();
    }

}

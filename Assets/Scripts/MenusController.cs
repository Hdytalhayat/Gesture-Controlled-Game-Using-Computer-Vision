using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenusController : MonoBehaviour
{
    [SerializeField] private Button retryButton, homeButton;

    public void Start()
    {   
        retryButton.onClick.AddListener(OnClickRetry);
        homeButton.onClick.AddListener(OnClickHome);
    }
    public void Update()
    {
        
    }
    private void OnClickRetry()
    {
        Scene currentScene = SceneManager.GetActiveScene();
            
        SceneManager.LoadScene(currentScene.name);
    }
    private void OnClickHome()
    {
        SceneManager.LoadScene("MainMenu");
    }

}

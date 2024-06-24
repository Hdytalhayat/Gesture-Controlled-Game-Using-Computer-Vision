using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MouseController : MonoBehaviour
{
    [SerializeField] private Button dynoBtn;
    // Start is called before the first frame update
    private void Start()
    {
        dynoBtn.onClick.AddListener(OnClickPlay);
    }

    private void OnClickPlay()
    {
        SceneManager.LoadScene("Game");
    }
}
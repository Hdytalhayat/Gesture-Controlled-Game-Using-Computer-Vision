using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public Button[] buttons;
    private int selectedButtonIndex = 0;
    private TesJarak tesJarak;
    // Start is called before the first frame update
    void Start()
    {
        SelectButton(selectedButtonIndex);
        tesJarak = GetComponent<TesJarak>();
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
        
        if(tesJarak.IsValid)
        {
            buttons[0].onClick.AddListener(DynoS);
            // buttons[1].onClick.AddListener(Flappy);
            // buttons[2].onClick.AddListener(Aims);
            // Mendeteksi input dari keyboard untuk mengubah pemilihan tombol

        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            ChangeSelectedButton(-1); // Pilih tombol sebelumnya
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            ChangeSelectedButton(1); // Pilih tombol berikutnya
        }

    }
    void ChangeSelectedButton(int direction)
    {
        // Menentukan indeks baru berdasarkan arah perubahan pemilihan (1 untuk ke bawah, -1 untuk ke atas)
        selectedButtonIndex += direction;
        // Pastikan indeks tetap dalam rentang yang valid
        selectedButtonIndex = Mathf.Clamp(selectedButtonIndex, 0, buttons.Length - 1);

        // Memilih tombol yang baru
        SelectButton(selectedButtonIndex);
    }

    void SelectButton(int index)
    {
        // Mengatur focus pada tombol yang dipilih
        EventSystem.current.SetSelectedGameObject(buttons[index].gameObject);
    }
}
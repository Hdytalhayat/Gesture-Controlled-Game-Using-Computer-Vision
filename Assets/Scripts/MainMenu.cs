using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public Button[] buttons;
    private int selectedButtonIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        SelectButton(selectedButtonIndex);
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
        // Mendeteksi input dari keyboard untuk mengubah pemilihan tombol
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ChangeSelectedButton(-1); // Pilih tombol sebelumnya
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
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
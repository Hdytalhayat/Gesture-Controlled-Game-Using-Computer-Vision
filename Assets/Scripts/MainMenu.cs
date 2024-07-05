using System.Collections;
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

    UDPReceive uDPReceive;
    string data;
    string[] points;

    //int delay = 60;

    public float delay = 2000f;
    int count = 0;


    void Start()
    {
        uDPReceive = GetComponent<UDPReceive>();
        data = uDPReceive.data;
        SelectButton(selectedButtonIndex);
        tesJarak = GetComponent<TesJarak>();
    }
    
    private void DynoS()
    {
        SceneManager.LoadScene("Game");
    }

    private void Supper()
    {
        SceneManager.LoadScene("Superman");
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
        DataHandle();
        if (tesJarak.IsValid)
        {
            buttons[0].onClick.AddListener(DynoS);
            buttons[1].onClick.AddListener(Supper);
            // buttons[2].onClick.AddListener(Aims);
            // Mendeteksi input dari keyboard untuk mengubah pemilihan tombol

        }

        if (points[9] == "True")
        {
            count++;
            Debug.Log("UP");
            if(count == 1)
            {
                ChangeSelectedButton(-1); // Pilih tombol sebelumnya
                
            }
            
        }
        else if (points[10] == "True")
        {
            count++;
            Debug.Log("DOWN");
            if(count == 1)
            {
                ChangeSelectedButton(1); // Pilih tombol berikutnya
            }
        }
        ResetCount();
        Debug.Log(count);
    }
    IEnumerator ResetCount()
    {
        yield return new WaitForSeconds(1);
        if(count != 0)
        {
            count = 0;
        }
    }
    private void DataHandle()
    {
        data = uDPReceive.data;
        // Remove the first and last character
        data = data.Remove(0, 1);
        data = data.Remove(data.Length - 1, 1);
        points = data.Split(", ");
        // print(points[0]+""+ points[1]); // Print the first point for debugging
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
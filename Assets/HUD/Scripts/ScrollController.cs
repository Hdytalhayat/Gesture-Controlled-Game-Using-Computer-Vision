using UnityEngine;
using UnityEngine.UI;

public class ScrollController : MonoBehaviour
{
    public Scrollbar scrollbar;
    public GameObject[] elements;
    public Image[] backgrounds; // Array untuk menyimpan background atau latar belakang masing-masing elemen

    float[] elementPositions;
    float distanceBetweenElements;

    void Start()
    {
        // Inisialisasi posisi elemen
        elementPositions = new float[elements.Length];
        distanceBetweenElements = 1f / (elements.Length - 1f);
    }

    void Update()
    {
        // Update posisi elemen
        for (int i = 0; i < elementPositions.Length; i++)
        {
            elementPositions[i] = distanceBetweenElements * i;
        }

        // Memanipulasi skala elemen dan background berdasarkan posisi scrollbar
        float scrollPosition = scrollbar.value;
        for (int i = 0; i < elementPositions.Length; i++)
        {
            if (scrollPosition < elementPositions[i] + (distanceBetweenElements / 2) && scrollPosition > elementPositions[i] - (distanceBetweenElements / 2))
            {
                // Skala elemen yang dipilih
                elements[i].transform.localScale = Vector2.Lerp(elements[i].transform.localScale, new Vector2(1.3f, 1.3f), 0.2f);
                // Skala elemen lainnya
                for (int j = 0; j < elementPositions.Length; j++)
                {
                    if (j != i)
                    {
                        elements[j].transform.localScale = Vector2.Lerp(elements[j].transform.localScale, new Vector2(0.8f, 0.8f), 0.1f);
                    }
                }

                // Ubah background yang terkait dengan elemen yang dipilih
                ChangeBackground(i);
            }
        }
    }

    // Fungsi untuk mengubah background berdasarkan indeks elemen yang dipilih
    void ChangeBackground(int selectedIndex)
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            // Aktifkan background yang sesuai dengan elemen yang dipilih
            backgrounds[i].gameObject.SetActive(i == selectedIndex);
        }
    }

    // Fungsi untuk menggeser pilihan ke kiri
    public void MoveSelectionLeft()
    {
        MoveSelection(-1);
    }

    // Fungsi untuk menggeser pilihan ke kanan
    public void MoveSelectionRight()
    {
        MoveSelection(1);
    }

    void MoveSelection(int direction)
    {
        // Cari indeks elemen yang dipilih saat ini
        int currentIndex = 0;
        float currentScrollPosition = scrollbar.value;

        for (int i = 0; i < elementPositions.Length; i++)
        {
            if (currentScrollPosition < elementPositions[i] + (distanceBetweenElements / 2) && currentScrollPosition > elementPositions[i] - (distanceBetweenElements / 2))
            {
                currentIndex = i;
                break;
            }
        }

        // Hitung indeks baru berdasarkan arah
        int newIndex = currentIndex + direction;
        newIndex = Mathf.Clamp(newIndex, 0, elementPositions.Length - 1);

        // Set posisi scrollbar
        scrollbar.value = elementPositions[newIndex];

        // Update skala sesuai dengan indeks baru
        // Skala elemen yang dipilih
        elements[newIndex].transform.localScale = new Vector2(1.3f, 1.3f);
        // Skala elemen lainnya
        for (int j = 0; j < elementPositions.Length; j++)
        {
            if (j != newIndex)
            {
                elements[j].transform.localScale = new Vector2(0.8f, 0.8f);
            }
        }

        // Ubah background sesuai dengan indeks baru
        ChangeBackground(newIndex);
    }
}

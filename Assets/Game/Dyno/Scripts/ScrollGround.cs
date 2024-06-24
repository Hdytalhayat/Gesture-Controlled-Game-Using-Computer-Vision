using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ScrollGround : MonoBehaviour
{
    public float resetPositionX = -10f; // Posisi X untuk mereset tanah
    public float startPositionX = 10f; // Posisi X awal tanah setelah reset

    private void Update()
    {
        // Periksa apakah permainan sedang aktif
        if (GameManager.Instance != null && GameManager.Instance.gameSpeed > 0)
        {
            // Gerakkan tanah ke kiri
            transform.Translate(Vector2.left * GameManager.Instance.gameSpeed * Time.deltaTime);

            // Cek apakah tanah sudah melewati posisi reset
            if (transform.position.x < resetPositionX)
            {
                // Reset posisi tanah ke awal
                Vector2 newPos = new Vector2(startPositionX, transform.position.y);
                transform.position = newPos;
            }
        }
    }
}

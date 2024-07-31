using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 1f;
    [SerializeField] private float resetPosition = -18f;
    [SerializeField] private float startPosition = 0f;

    private void Update()
    {
        // Gerakkan background ke kiri
        transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);

        // Jika background sudah bergerak cukup jauh ke kiri, pindahkan kembali ke posisi awal
        if (transform.position.x <= resetPosition)
        {
            transform.position = new Vector3(startPosition, transform.position.y, transform.position.z);
        }
    }
}
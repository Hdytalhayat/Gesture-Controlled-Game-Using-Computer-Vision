using System.Collections;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float parallaxEffect; // Faktor efek parallax, misalnya 0.5 untuk latar belakang yang bergerak lebih lambat

    private float length, startPosX, posX = 1;

    public GameObject sprite;

    void Start()
    {
        // Menyimpan posisi awal dan panjang latar belakang
        startPosX = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        StartCoroutine(Spawn());
    }

    void Update()
    {
        posX -= 0.001F;
        // Menghitung jarak yang telah ditempuh oleh target
        float distance = posX * (1 - parallaxEffect);

        // Menghitung offset
        float offset = posX * parallaxEffect;

        // Menggerakkan latar belakang
        transform.position = new Vector3(startPosX + offset, transform.position.y, transform.position.z);

        // Mengulang latar belakang jika keluar dari tampilan kamera
        if (distance > startPosX + length)
        {
            startPosX += length;
        }
        else if (distance < startPosX - length)
        {
            startPosX -= length;
        }
    }
    IEnumerator Spawn()
    {
        while(true)
        {
            yield return new WaitForSeconds(2f);
            Instantiate(sprite, new Vector3(transform.position.x +1, transform.position.y, transform.position.z), Quaternion.identity);

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BeatSpawner : MonoBehaviour
{
    public float beatTempo;
    public GameObject[] spawnableObject;
    private float nextSpawnTime;
    // Start is called before the first frame update
    void Start()
    {
        beatTempo = beatTempo/60f;
        nextSpawnTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextSpawnTime)
        {
            Spawn();
            nextSpawnTime = Time.time + 2f;
        }
    }

    void Spawn()
    {
        int randomIndex = Random.Range(0, spawnableObject.Length);
        GameObject spawnedObject = Instantiate(spawnableObject[randomIndex], transform.position, Quaternion.identity);
        spawnedObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-beatTempo, 0f);
    }
}

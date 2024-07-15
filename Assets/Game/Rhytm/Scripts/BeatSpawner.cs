using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BeatSpawner : MonoBehaviour
{
    public float beatTempo;
    public GameObject[] spawnableObject;
    private float nextSpawnTime;
    private float spawnInterval = 2f;
    private float movementSpeedMultiplier = 1f;
    private float spawnRateMultiplier = 1f;
    private float timeElapsed = 0f;
    // Start is called before the first frame update
    void Start()
    {
        beatTempo = beatTempo/60f;
        nextSpawnTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed > 10f) // adjust this value to control the rate of increase
        {
            movementSpeedMultiplier = Mathf.Clamp(movementSpeedMultiplier, movementSpeedMultiplier + 0.1f, 2.5f);
            spawnRateMultiplier = Mathf.Clamp(spawnRateMultiplier, spawnRateMultiplier + 0.1f, 1.5f);
            spawnInterval -= 0.1f; // adjust this value to control the rate of decrease
            timeElapsed = 0f;
        }

        if (Time.time > nextSpawnTime)
        {
            Spawn();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void Spawn()
    {
        int randomIndex = Random.Range(0, spawnableObject.Length);
        GameObject spawnedObject = Instantiate(spawnableObject[randomIndex], transform.position, Quaternion.identity);
        spawnedObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-beatTempo * movementSpeedMultiplier, 0f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objects_Spawning : MonoBehaviour
{
    public float maxSpawnTime=5f;
    float spawningTime;
    public GameObject targetObject;
    public GameObject gun;
    // Start is called before the first frame update
    void Start()
    {
        spawnTarget();
        gun.GetComponent<Gun_Script>().on_TargetHit += Objects_Spawning_on_TargetHit;
        
    }

    private void Objects_Spawning_on_TargetHit(object sender, System.EventArgs e)
    {
        spawnTarget();
    }

    void spawnTarget()
    {
        float x = Random.Range(-11f, 11f);
        float y = Random.Range(9.5f, 16.5f);
        float z = Random.Range(17f, 30f);
        Object.Instantiate(targetObject, new Vector3(x, y, z), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        //spawningTime -= Time.deltaTime;
        //if (spawningTime <= 0)
        //{
        //    float x = Random.Range(-11f, 11f);
        //    float y = Random.Range(9.5f, 16.5f);
        //    float z = Random.Range(17f, 30f);
        //    Object.Instantiate(targetObject, new Vector3(x, y, z),Quaternion.identity);
        //    spawningTime = maxSpawnTime;
        //}
    }
}

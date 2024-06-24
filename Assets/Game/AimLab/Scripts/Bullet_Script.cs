using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Script : MonoBehaviour
{
    // Start is called before the first frame update

    private Vector3 startPosition = new Vector3(1f, 11.8f, 11.5f);
    private Vector3 endPosition;
    private float timeElapsed;
    public float totalTime = 1f;

    public void moveBullet(Vector3 start_Position,Vector3 end_Position)
    {
        endPosition = end_Position;
        startPosition = start_Position;
    }

    // Update is called once per frame
    void Update()
    {
        movingBullet();
    }
    
    void movingBullet()
    {
        timeElapsed += Time.deltaTime;
        float interpolationValue = timeElapsed / totalTime;

        transform.position = Vector3.Lerp(startPosition, endPosition, interpolationValue);
    }
}

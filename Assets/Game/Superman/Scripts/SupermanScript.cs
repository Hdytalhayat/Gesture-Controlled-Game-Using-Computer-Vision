
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupermanScript : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    public float flyingStrength;
    public LogicManagerScript logic;
    public bool supermanIsAlive = true;
    private int n = 0;

    [SerializeField]
    private float maxAngle;


    UDPReceive uDPReceive;
    string data;
    string[] points;

    // Start is called before the first frame update
    void Start()
    {
        uDPReceive = GetComponent<UDPReceive>();
        data = uDPReceive.data;
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicManagerScript>();
    }

    public void UpdateSupermanAngle()
    {
        if (supermanIsAlive)
        {
            //Get  percentage of speed
            float percentage = Mathf.InverseLerp(-flyingStrength, flyingStrength, myRigidBody.velocity.y);

            //Determines value of angle
            float angle = Mathf.Lerp(-maxAngle, maxAngle, percentage);

            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    // Update is called once per frame
    void Update()
    {
        DataHandle();

        if (Input.GetKeyDown(KeyCode.Space) == true && supermanIsAlive==true || points[1] == "True")
        {
            myRigidBody.velocity = Vector2.up * flyingStrength;
            SoundManagerScript.PlaySound("Jump");
        }

        if(myRigidBody.transform.position.y > 15)
        {
            GameOver();
        }

        if (myRigidBody.transform.position.y < -15)
        {
            GameOver();
        }


        UpdateSupermanAngle();
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameOver();
    }

    private void GameOver()
    {
        if (n == 0)
        {
            SoundManagerScript.PlaySound("Dead");
            n++;
        }
        logic.gameOver();
        supermanIsAlive = false;
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

/// <summary>
/// Spritesheet for Flappy Bird found here: http://www.spriters-resource.com/mobile_phone/flappybird/sheet/59537/
/// Audio for Flappy Bird found here: https://www.sounds-resource.com/mobile/flappybird/sound/5309/
/// </summary>
public class FlappyScript : MonoBehaviour
{
    // DataHandle
    private UDPReceive uDPReceive;
    private string data;
    private string[] points = new string[3];

    public AudioClip FlyAudioClip, DeathAudioClip, ScoredAudioClip;
    public Sprite GetReadySprite;
    public float RotateUpSpeed = 1, RotateDownSpeed = 1;
    public GameObject IntroGUI, DeathGUI;
    public Collider2D restartButtonGameCollider;
    public float VelocityPerJump = 3;
    public float XSpeed = 1;

    private Rigidbody2D rb;
    private FlappyYAxisTravelState flappyYAxisTravelState;

    private enum FlappyYAxisTravelState
    {
        GoingUp, GoingDown
    }

    private Vector3 birdRotation = Vector3.zero;

    // Use this for initialization
    void Start()
    {
        uDPReceive = GetComponent<UDPReceive>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        DataHandle();

        // Handle back key in Windows Phone
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        if (GameStateManager.GameState == GameState.Intro)
        {
            MoveBirdOnXAxis();
            if (WasTouchedOrClicked())
            {
                BoostOnYAxis();
                GameStateManager.GameState = GameState.Playing;
                IntroGUI.SetActive(false);
                ScoreManagerScript.Score = 0;
            }
        }
        else if (GameStateManager.GameState == GameState.Playing)
        {
            MoveBirdOnXAxis();
            if (WasTouchedOrClicked())
            {
                BoostOnYAxis();
            }
        }
        else if (GameStateManager.GameState == GameState.Dead)
        {
            Vector2 contactPoint = Vector2.zero;

            if (Input.touchCount > 0)
                contactPoint = Input.touches[0].position;
            if (Input.GetMouseButtonDown(0))
                contactPoint = Input.mousePosition;

            // Check if user wants to restart the game
            if (restartButtonGameCollider == Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(contactPoint)))
            {
                GameStateManager.GameState = GameState.Intro;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    void FixedUpdate()
    {
        // Just jump up and down on intro screen
        if (GameStateManager.GameState == GameState.Intro)
        {
            if (rb.velocity.y < -1) // When the speed drops, give a boost
                rb.AddForce(new Vector2(0, rb.mass * 5500 * Time.deltaTime)); // Lots of play and stop 
                                                                              // and play and stop etc to find this value, feel free to modify
        }
        else if (GameStateManager.GameState == GameState.Playing || GameStateManager.GameState == GameState.Dead)
        {
            FixFlappyRotation();
        }
    }

    bool WasTouchedOrClicked()
    {
        return points.Length > 1 && points[1] == "True" || Input.GetButtonUp("Jump") || Input.GetMouseButtonDown(0) || 
            (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended);
    }

    private void DataHandle()
    {
        try
        {
            data = uDPReceive.data;

            if (!string.IsNullOrEmpty(data))
            {
                // Remove the first and last character
                data = data.Substring(1, data.Length - 2);
                points = data.Split(new string[] { ", " }, System.StringSplitOptions.None);
                // print(points[0] + "" + points[1]); // Print the first point for debugging
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("DataHandle error: " + ex.Message);
        }
    }

    void MoveBirdOnXAxis()
    {
        transform.position += new Vector3(Time.deltaTime * XSpeed, 0, 0);
    }

    void BoostOnYAxis()
    {
        rb.velocity = new Vector2(0, VelocityPerJump);
        GetComponent<AudioSource>().PlayOneShot(FlyAudioClip);
    }

    /// <summary>
    /// When the flappy goes up, it'll rotate up to 45 degrees. when it falls, rotation will be -90 degrees min
    /// </summary>
    private void FixFlappyRotation()
    {
        if (rb.velocity.y > 0)
            flappyYAxisTravelState = FlappyYAxisTravelState.GoingUp;
        else
            flappyYAxisTravelState = FlappyYAxisTravelState.GoingDown;

        float degreesToAdd = 0;

        switch (flappyYAxisTravelState)
        {
            case FlappyYAxisTravelState.GoingUp:
                degreesToAdd = 6 * RotateUpSpeed;
                break;
            case FlappyYAxisTravelState.GoingDown:
                degreesToAdd = -3 * RotateDownSpeed;
                break;
        }

        // Clamp the values so that -90 < rotation < 45 *always*
        birdRotation = new Vector3(0, 0, Mathf.Clamp(birdRotation.z + degreesToAdd, -90, 45));
        transform.eulerAngles = birdRotation;
    }

    /// <summary>
    /// Check for collision with pipes
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerEnter2D(Collider2D col)
    {
        if (GameStateManager.GameState == GameState.Playing)
        {
            if (col.gameObject.tag == "Pipeblank") // Pipeblank is an empty gameobject with a collider between the two pipes
            {
                GetComponent<AudioSource>().PlayOneShot(ScoredAudioClip);
                ScoreManagerScript.Score++;
            }
            else if (col.gameObject.tag == "Pipe")
            {
                FlappyDies();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (GameStateManager.GameState == GameState.Playing)
        {
            if (col.gameObject.tag == "Floor")
            {
                FlappyDies();
            }
        }
    }

    void FlappyDies()
    {
        GameStateManager.GameState = GameState.Dead;
        DeathGUI.SetActive(true);
        GetComponent<AudioSource>().PlayOneShot(DeathAudioClip);
    }
}

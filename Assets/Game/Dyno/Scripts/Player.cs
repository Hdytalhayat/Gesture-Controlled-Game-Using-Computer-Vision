using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    private CharacterController character;
    private Vector3 direction;

    public float jumpForce = 8f;
    public float gravity = 9.81f * 2f;

    //DataHandle
    UDPReceive uDPReceive;
    string data;
    string[] points;

    private void Awake()
    {
        uDPReceive = GetComponent<UDPReceive>();
        data = uDPReceive.data;
        character = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        direction = Vector3.zero;
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
    private void Update()
    {
        DataHandle();
    
        direction += gravity * Time.deltaTime * Vector3.down;
        if (character.isGrounded)
        {
            direction = Vector3.down;

            if (Input.GetKeyDown(KeyCode.Space)) {
                direction = Vector3.up * jumpForce;
            }
        }

        character.Move(direction * Time.deltaTime);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle")) {
            GameManager.Instance.GameOver();
        }
    }

}
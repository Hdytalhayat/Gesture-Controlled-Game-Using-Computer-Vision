using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTracking : MonoBehaviour
{
    [SerializeField] private Hand_Detector_UDPReceive uDPReceive; // Reference to the UDPReceive script
    [SerializeField] private GameObject[] handPoints; // Array of GameObjects representing hand points
    [SerializeField] private GameObject titik; // A specific GameObject to be moved

    void Update()
    {
        string data = uDPReceive.data; // Get the data from UDPReceive
        if (string.IsNullOrEmpty(data) || data.Length < 2)
        {
            return; // If there's no data or data length is less than 2, do nothing
        }

        // Remove the first and last character
        data = data.Remove(0, 1);
        data = data.Remove(data.Length - 1, 1);
        // Split the data into an array of strings
        string[] points = data.Split(',');
        print(points[0]); // Print the first point for debugging

        if (points.Length != handPoints.Length * 3)
        {
            Debug.LogWarning("Data length does not match the number of hand points.");
            return; // Ensure the data length matches the number of hand points
        }

        // Iterate through each hand point and update its position
        for (int i = 0; i < handPoints.Length; i++)
        {
            // Parse the x, y, z coordinates
            float x = 7 - float.Parse(points[i * 3]) / 100;
            float y = float.Parse(points[i * 3 + 1]) / 100;
            float z = float.Parse(points[i * 3 + 2]) / 100;

            // Update the position of the hand point
            handPoints[i].transform.localPosition = new Vector3(x, y, z);
        }
            // titik.transform.position = handPoints[0].transform.Position;
    }
}

using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class OpenCVTest : MonoBehaviour
{
    [DllImport("opencv_world410", EntryPoint = "cvFunction")]
    private static extern void CvFunction();

    void Awake()
    {
        CvFunction();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ParallaxEffect : MonoBehaviour
{
    Transform cam;
    Vector3 camStartPos;
    float distance;

    GameObject[] backgrounds;
    Material[] mat;
    float[] backSpeed;

    float fartherstBack;

    [Range(0.01f,0.05f)]
    public float parallaxSpeed;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
        camStartPos = cam.position;

        int backCout = transform.childCount;
        mat = new Material[backCout];
        backSpeed = new float[backCout];
        backgrounds = new GameObject[backCout];

        for (int i = 0; i < backCout; i++)
        {
            backgrounds[i] = transform.GetChild(i).gameObject;
            mat[i] = backgrounds[i].GetComponent<Renderer>().material;
        }
    }

    void BackSpeedCalculate(int backCount)
    {
        for (int i = 0; i < backCount; i++)
        {
            if ((backgrounds))
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

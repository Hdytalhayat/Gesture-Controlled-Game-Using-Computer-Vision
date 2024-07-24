using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class ScrollController : MonoBehaviour
{
    public GameObject scrollBar;
    public Image background; // Reference to your canvas background image component

    float scroll_pos = 0;
    float[] pos;

    void Start()
    {
        // Initialize pos array based on child count
        pos = new float[transform.childCount];
    }

    void Update()
    {
        float distance = 1f / (pos.Length - 1f);

        // Update pos array based on child positions
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
        }

        // Check scroll input
        if (Input.GetMouseButton(0))
        {
            scroll_pos = scrollBar.GetComponent<Scrollbar>().value;
        }
        else
        {
            // Adjust scrollbar value based on current position
            for (int i = 0; i < pos.Length; i++)
            {
                if (scroll_pos < pos[i] * (distance / 2) && scroll_pos > pos[i] - (distance / 2))
                {
                    scrollBar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollBar.GetComponent<Scrollbar>().value, pos[i], 0.1f);
                }
            }
        }

        // Scale and background color change based on scroll position
        for (int i = 0; i < pos.Length; i++)
        {
            if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
            {
                // Change the scale of the child objects
                transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(1.3f, 1.3f), 0.2f);

                // Change background color (example: based on index i)
                if (background != null)
                {
                    if (i % 2 == 0)
                    {
                        background.color = Color.blue;
                    }
                    else
                    {
                        background.color = Color.red;
                    }
                }

                // Reset other child scales
                for (int a = 0; a < pos.Length; a++)
                {
                    if (a != i)
                    {
                        transform.GetChild(a).localScale = Vector2.Lerp(transform.GetChild(a).localScale, new Vector2(0.8f, 0.8f), 0.1f);
                    }
                }
            }
        }
    }
}

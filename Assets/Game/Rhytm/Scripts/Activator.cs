using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{
    public bool redNoteActivator = false;
    public bool blueNoteActivator = false;

    void Start()
    {
        redNoteActivator = false;
        blueNoteActivator = false;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("red_note") || col.CompareTag("bigred_note"))
        {
            redNoteActivator = true;
            blueNoteActivator = false;
        }
        else if (col.CompareTag("blue_note") || col.CompareTag("bigblue_note"))
        {
            blueNoteActivator = true;
            redNoteActivator = false;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("red_note") || col.CompareTag("bigred_note"))
        {
            redNoteActivator = false;
        }
        else if (col.CompareTag("blue_note") || col.CompareTag("bigblue_note"))
        {
            blueNoteActivator = false;
        }
    }
}

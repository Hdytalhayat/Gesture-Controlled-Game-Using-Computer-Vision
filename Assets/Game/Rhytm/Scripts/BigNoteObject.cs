using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigNoteObject : MonoBehaviour
{
    public KeyCode[] redKeys = new KeyCode[2] { KeyCode.X, KeyCode.N };
    public KeyCode[] blueKeys = new KeyCode[2] { KeyCode.Z, KeyCode.M };

    public bool canBePressed;
    private bool alreadyProcessed;
    private bool isBigRedNote;
    private bool isBigBlueNote;

    private string triggerTag;

    private float redKeyTime = 0;
    private float blueKeyTime = 0;

    void Start()
    {
        alreadyProcessed = false;
        isBigRedNote = gameObject.CompareTag("bigred_note");
        isBigBlueNote = gameObject.CompareTag("bigblue_note");
    }

    void Update()
    {
        if (Input.GetKeyDown(redKeys[0]) || Input.GetKeyDown(redKeys[1]))
        {
            redKeyTime = Time.time;
        }
        if (Input.GetKeyDown(redKeys[1]) || Input.GetKeyDown(redKeys[0]))
        {
            if (Time.time - redKeyTime < 1)
            {
                HandleKeyPress(isBigRedNote);
            }
        }

        if (Input.GetKeyDown(blueKeys[0]) || Input.GetKeyDown(blueKeys[1]))
        {
            blueKeyTime = Time.time;
        }
        if (Input.GetKeyDown(blueKeys[1]) || Input.GetKeyDown(blueKeys[0]))
        {
            if (Time.time - blueKeyTime < 1)
            {
                HandleKeyPress(isBigBlueNote);
            }
        }
        if (alreadyProcessed)
        {
            Destroy(gameObject);
        }
    }

    private void HandleKeyPress(bool isNote)
    {
        if (canBePressed && !alreadyProcessed)
        {
            if (isNote)
            {
                Note(triggerTag);
            }
            else
            {
                GManager.instance.NoteMiss();
                alreadyProcessed = true;
            }
        }
    }

    private void Note(string triggerTag)
    {
        alreadyProcessed = true;
        if (isBigRedNote || isBigBlueNote)
        {
            if (triggerTag == "Activator")
            {
                GManager.instance.PerfectHit();
            }
            else if (triggerTag == "goodActivator")
            {
                GManager.instance.GoodHit();
            }
        }
        Destroy(gameObject);
    }



    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Activator") || col.CompareTag("goodActivator"))
        {
            canBePressed = true;
            triggerTag = col.tag;
        }

        if (col.CompareTag("spawn"))
        {
            if (this)
            {
                this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Activator"))
        {
            canBePressed = false;
            if (!alreadyProcessed)
            {
                if (isBigRedNote)
                {
                    GManager.instance.NoteMiss();
                }
                else if (isBigBlueNote)
                {
                    GManager.instance.NoteMiss();
                }
                alreadyProcessed = true;
            }
        }

        if (col.CompareTag("goodActivator"))
        {
            canBePressed = false;
        }
    }
}
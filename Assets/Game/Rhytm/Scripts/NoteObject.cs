using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public KeyCode[] redKeys = new KeyCode[2] { KeyCode.X, KeyCode.N };
    public KeyCode[] blueKeys = new KeyCode[2] { KeyCode.Z, KeyCode.M };

    public bool canBePressed;
    private bool alreadyProcessed;
    private bool isRedNote;
    private bool isBlueNote;


    private string triggerTag;

    void Start()
    {
        alreadyProcessed = false;
        isRedNote = gameObject.CompareTag("red_note");
        isBlueNote = gameObject.CompareTag("blue_note");
    }

    void Update()
    {
        if (Input.GetKeyDown(redKeys[0]) || Input.GetKeyDown(redKeys[1]))
        {
            HandleKeyPress(isRedNote);
        }

        else if (Input.GetKeyDown(blueKeys[0]) || Input.GetKeyDown(blueKeys[1]))
        {
            HandleKeyPress(isBlueNote);
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
        if (isRedNote || isBlueNote)
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
                if (isRedNote)
                {
                    GManager.instance.NoteMiss();
                }
                else if (isBlueNote)
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
using System.Collections;
using UnityEngine;

public class Player1Controller : MonoBehaviour
{
    public Animator animator;
    [SerializeField] private GameObject player2Controller;

    // Sound effects


    // Timers
    public float hitDelay = 0.1f;
    public float hitInterval = 1.0f;

    private bool canHit = true;

    void Start()
    {
    
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (canHit && Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(DelayedKnockback());
            canHit = false;
            StartCoroutine(HitIntervalTimer());
        }

        if (Input.GetKey(KeyCode.W))
        {
            animator.SetBool("Defence", true);
        }
        else
        {
            animator.SetBool("Defence", false);
        }
    }

    IEnumerator DelayedKnockback()
    {
        animator.SetTrigger("Hit");
      

        yield return new WaitForSeconds(hitDelay);
        
        // Check if Player 2 is defending
        if (!player2Controller.GetComponent<Animator>().GetBool("Defence"))
        {
            player2Controller.GetComponent<Player2Controller>().PlayKnockback();
        }
    }

    IEnumerator HitIntervalTimer()
    {
        yield return new WaitForSeconds(hitInterval);
        canHit = true;
    }

    public void PlayKnockback()
    {
        if (!player2Controller.GetComponent<Animator>().GetBool("Defence"))
        {
           
            animator.SetTrigger("Knockback");
        }
    }
}

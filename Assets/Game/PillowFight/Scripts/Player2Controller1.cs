using System.Collections;
using UnityEngine;

public class Player2Controller : MonoBehaviour
{
    public Animator animator;
    [SerializeField] private GameObject player1Controller;
    public float hitDelay = 0.1f;
    public float hitInterval = 1.0f;

    private bool canHit = true;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (canHit && Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(DelayedKnockback());
            canHit = false;
            StartCoroutine(HitIntervalTimer());
        }

        if (Input.GetKey(KeyCode.O))
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
        
        // Check if Player 1 is defending
        if (!player1Controller.GetComponent<Animator>().GetBool("Defence"))
        {
            player1Controller.GetComponent<Player1Controller>().PlayKnockback();
        }
    }

    IEnumerator HitIntervalTimer()
    {
        yield return new WaitForSeconds(hitInterval);
        canHit = true;
    }

    public void PlayKnockback()
    {
        if (!player1Controller.GetComponent<Animator>().GetBool("Defence"))
        {
            animator.SetTrigger("Knockback");
        }
    }
}

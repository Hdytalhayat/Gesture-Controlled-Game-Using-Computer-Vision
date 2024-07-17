using System.Collections;
using UnityEngine;

public class Player1Controller : MonoBehaviour
{
    public Animator animator;
    [SerializeField] private GameObject player2Controller;

    // Sound effects
    public AudioSource audioSource;
    public AudioClip hitSFX;
    public AudioClip knockbackSFX;

    // Timers
    public float hitDelay = 0.1f;
    public float hitInterval = 1.0f;

    private bool canHit = true;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
        if (!GetComponent<Animator>().GetBool("Defence"))
        {
            audioSource.PlayOneShot(hitSFX);
        }
        animator.SetTrigger("Hit");
        audioSource.PlayOneShot(hitSFX);

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
            audioSource.PlayOneShot(knockbackSFX); // Play knockback SFX
            animator.SetTrigger("Knockback");
        }
    }
}

using System.Collections;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class Player1Controller : MonoBehaviour
{
    public Animator animator;
    [SerializeField] private GameObject player2Controller;

    // health

    public int maxHealth = 100;
	public int currentHealth;

	public HealthBar healthBar;


    // Timers
    public float hitDelay = 0.1f;
    public float hitInterval = 1.0f;

    private bool canHit = true;
    Rigidbody2D rb2d;
    void Start()
    {
        currentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(currentHealth <= 0)
        {
            animator.SetTrigger("Dead");
            rb2d.gravityScale = 1;
        }
        else
        {
            animator.SetBool("IsDead", false);
            rb2d.gravityScale = 0;

        }
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

    void TakeDamage(int damage)
	{
		currentHealth -= damage;

		healthBar.SetHealth(currentHealth);
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
            TakeDamage(10);
        }
        if(currentHealth <= 0)
        {
        }
    }
}

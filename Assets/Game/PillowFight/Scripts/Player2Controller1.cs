using System.Collections;
using UnityEngine;

public class Player2Controller : MonoBehaviour
{
    public Animator animator;
    [SerializeField] private GameObject player1Controller;
    public float hitDelay = 0.1f;
    public float hitInterval = 1.0f;


     public int maxHealth = 100;
	public int currentHealth;

	public HealthBar healthBar;

    private bool canHit = true;// Sound effects

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
        if (!player1Controller.GetComponent<Animator>().GetBool("Defence"))
        {
      
            animator.SetTrigger("Knockback");
            TakeDamage(10);
        }
    }
}

using System.Collections;
using UnityEngine;
using TMPro;
public class Player2Controller : MonoBehaviour
{
    public Animator animator;
    [SerializeField] private GameObject player1Controller;
    public float hitDelay = 0.1f;
    public float hitInterval = 1.0f;
    public GameObject notFound;

     public int maxHealth = 100;
	public int currentHealth;
    private bool isDead = false;

	public HealthBar healthBar;

    private bool canHit = true;// Sound effects

    Rigidbody2D rb2d;

    public GameObject udp;
    UDPReceive uDPReceive;
    string data;
    string[] points;
    
    public bool isActive = false;
    private void DataHandle()
    {
        data = uDPReceive.data;
        // Remove the first and last character
        data = data.Remove(0, 1);
        data = data.Remove(data.Length - 1, 1);
        points = data.Split(", ");
        // print(points[0]+""+ points[1]); // Print the first point for debugging
    }
    void Start()
    {
        uDPReceive = udp.GetComponent<UDPReceive>();
        currentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);
        animator = GetComponent<Animator>();
        
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        DataHandle();
        if (int.Parse(points[4]) == 0)
        {
            notFound.SetActive(true);
            isActive = false;
        }
        else{
            notFound.SetActive(false);
            isActive = true;
        }
        if(player1Controller.GetComponent<Player1Controller>().isActive)
        {
            if (!isDead)  // Check if the player is not already dead
            {
                if (currentHealth <= 0)
                {
                    animator.SetTrigger("Dead");
                    rb2d.gravityScale = 1;
                    isDead = true;  // Mark the player as dead
                    return;
                }
                else
                {
                    rb2d.gravityScale = 0;
                }

                if (canHit && (Input.GetKeyDown(KeyCode.P) || points[7] == "'hit'"))
                {
                    StartCoroutine(DelayedKnockback());
                    canHit = false;
                    StartCoroutine(HitIntervalTimer());
                }

                if (Input.GetKey(KeyCode.O) || points[7] == "'def'")
                {
                    animator.SetBool("Defence", true);
                }
                else
                {
                    animator.SetBool("Defence", false);
                }
            }

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
        if (!isDead && !player1Controller.GetComponent<Animator>().GetBool("Defence"))
        {
      
            animator.SetTrigger("Knockback");
            TakeDamage(10);
        }
    }
}

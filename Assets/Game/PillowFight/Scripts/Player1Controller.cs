using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Player1Controller : MonoBehaviour
{
    public Animator animator;
    [SerializeField] private GameObject player2Controller;
    [SerializeField] private SpriteRenderer pipe;
    public float hitDelay = 0.1f;
    public float hitInterval = 1.0f;
    public GameObject notFound;

    public int maxHealth = 100;
	public int currentHealth;
    public bool isDead = false;

	public HealthBar healthBar;

    private bool canHit = true;

    Rigidbody2D rb2d;

    public GameObject udp;
    UDPReceive uDPReceive;
    string data;
    string[] points;

    public bool isActive = false;

    // [SerializeField] private GameObject pauseMenu;
    public bool isPauseActive; 
    public bool canMove;

    public TutorialController tutorialController;
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
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        pipe.enabled = false;
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
        if(player2Controller.GetComponent<Player2Controller>().isActive)
        {
            if (!isDead && canMove)  // Check if the player is not already dead
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
                if (canHit && (Input.GetKeyDown(KeyCode.Q) || points[6] == "'hit'"))
                {
                    StartCoroutine(DelayedKnockback());
                    canHit = false;
                    StartCoroutine(HitIntervalTimer());
                }

                if (Input.GetKey(KeyCode.W)|| points[6] == "'def'")
                {
                    animator.SetBool("Defence", true);
                }
                else
                {
                    animator.SetBool("Defence", false);
                }
            }

        }
        // pauseMenu.SetActive(isPauseActive);
        if(isPauseActive || tutorialController.IsTutorial)
        {
            canMove = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            player2Controller.GetComponent<SpriteRenderer>().enabled = false;
            pipe.enabled = false;
        }
        else if(!isPauseActive || !tutorialController.IsTutorial)
        {
            canMove = true;
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            player2Controller.GetComponent<SpriteRenderer>().enabled = true;
            pipe.enabled = true;

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
        if (!isDead && !player2Controller.GetComponent<Animator>().GetBool("Defence"))
        {
           
            animator.SetTrigger("Knockback");
            TakeDamage(10);
        }
    }
    

}

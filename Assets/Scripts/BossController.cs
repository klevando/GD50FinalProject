using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossController : MonoBehaviour {

    public float bossSpeed;
    public bool toRight = true;
    public float movementDuration;
    public float moveTimer;

    // reference to animator to grab animation
    //private Animator animator;
    private Rigidbody2D rb;

    // ravanas health
    public int bossHealth;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();

        // randomly assign a speed and a length of movement
        SetBossMovement();

        bossHealth = 20;
	}
	
	void FixedUpdate () {
        // script to move the boss randomly back and forth
        if (moveTimer < movementDuration) {
            if (toRight)
            {
                rb.velocity = new Vector2(bossSpeed, rb.velocity.y);
                if (moveTimer <= 0) {
                    SetBossMovement();
                    moveTimer = movementDuration;
                    toRight = false;
                }

                //animator.SetFloat("Sita_Speed", Mathf.Abs(horizontal));
                //playerComponents.knockbackTimer = playerComponents.knockbackLength;
            }
            else
            {
                rb.velocity = new Vector2(-bossSpeed, rb.velocity.y);
                if (moveTimer <= 0) {
                    SetBossMovement();
                    moveTimer = movementDuration;
                    toRight = true;
                }

            }

            // could place a script here to change toRight to !toRight if hits wall here
        }
	}

    // Update is called once per frame
	void Update () {
        moveTimer -= Time.deltaTime;

        if (bossHealth <= 0) {
            bossHealth = 0;
            SceneManager.LoadScene("End");
        } 
	}

    void SetBossMovement() {
        movementDuration = Random.Range(0.3f, 6.3f);
        bossSpeed = Random.Range(1f, 3f);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Projectile")) {
            bossHealth -= 1;
        }
    }
}

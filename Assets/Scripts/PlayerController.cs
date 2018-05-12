using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    // movement variables
    public float maxSpeed = 10f;
    private float vertical, horizontal;

    // sprite drawn facing only right, and starts on scene facing right
    public bool toRight = true;

    // character starts above the ground, and we want to keep track when we are touching the ground
    // groundCheck.position is where circle is generated
    // groundRadius is the radius of this circle
    // trueGround is a layer mask, all the things our circle will collide with
    bool onGround = false;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask trueGround;

    // player health variables
    public int playerCurrentHealth;
    public int playerMaxHealth = 6;

    // gem counter
    public int gemTotal;

    //knockback variables
    public float knockback;
    public float knockbackLength;
    public float knockbackTimer;
    public bool knockbackFromRight;

    //bounch vairables
    public float bounce;
    public float bounceLength;
    public float bounceTimer;

    // projecile variables
    public Transform firingPosition;
    public GameObject fireBall;
    public float fireBallSpeed;
    public bool fireBallDirection;

    // reference to player animator to grab animation
    private Animator animator;

    // reference to player rigidbody
    private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // start player with full life
        playerCurrentHealth = playerMaxHealth;

        // initalize gem count
        gemTotal = 0;

        // initalize fireball speed
        fireBallSpeed = 4f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        // check if player is on ground - are we hitting colliders in this cirlce?
        onGround = Physics2D.OverlapCircle(groundCheck.position, groundRadius, trueGround);
        animator.SetBool("Ground", onGround);

        // vertical speed - relevant to jump animation
        animator.SetFloat("VerticalSpeed", rb.velocity.y);

        // move player right to left when player presses right or left if they haven't been knocked back
        horizontal = Input.GetAxis("Horizontal");
        if (knockbackTimer <= 0) {
            rb.velocity = new Vector2(horizontal * maxSpeed, rb.velocity.y);
            animator.SetFloat("PlayerSpeed", Mathf.Abs(horizontal));
        }
        else if (knockbackTimer > 0) {
            if (knockbackFromRight) {
                rb.velocity = new Vector2(knockback, knockback);
            }
            else {
                rb.velocity = new Vector2(-knockback, knockback);
            }
        }

        if (bounceTimer > 0) {
            rb.AddForce(new Vector2(0, 1), ForceMode2D.Impulse);
        }


        if (horizontal > 0 && !toRight) {
            Flip();
        }
        else if (horizontal < 0 && toRight) {
            Flip();
        }
	}

    // using update for jumping because we dont want to miss events
	private void Update() {
        // if spacebar is pressed then, character jumps
        // use ForceMode2D.Impulse to have a faster jump
        if (onGround && Input.GetButtonDown("Jump")) {
            animator.SetBool("Ground", false);
            rb.AddForce(new Vector2(0, 12), ForceMode2D.Impulse);
        }

        if (Input.GetButtonDown("Fire1")) {
            if (toRight) {
                //Instantiate(fireBall, firingPosition.position, Quaternion.Euler(0, 0, 0));
                Instantiate(fireBall, firingPosition.position, Quaternion.Euler(new Vector3(0,0,0)));
                fireBallDirection = true;

            }
            else {
                //Instantiate(fireBall, firingPosition.position, Quaternion.Euler(0,0,180f));
                Instantiate(fireBall, firingPosition.position, Quaternion.Euler(new Vector3(0,0,180)));
                fireBallDirection = false;
            }

        } 

        if (playerCurrentHealth <= 0) {
            playerCurrentHealth = 0;
            PlayerDeath();
        }
        else if (playerCurrentHealth >= playerMaxHealth) {
            playerCurrentHealth = playerMaxHealth;
        }

        // timer for knockbacks
        knockbackTimer -= Time.deltaTime;

        // timer for bouncing
        bounceTimer -= Time.deltaTime;
	}

	// function to flip sprite that is drawn facing only one direction
	void Flip () {
        toRight = !toRight;

        // get local scale
        Vector3 scale = transform.localScale;

        //flip x axis
        scale.x = scale.x * -1;
        transform.localScale = scale;
    }

    // player death funtion
    void PlayerDeath () {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene, LoadSceneMode.Single);
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Gem")) {
            gemTotal += 1;
        }
        else if (collision.CompareTag("NonConsumableHeart")) {
            PlayerLife(playerMaxHealth);
        }
        else if (collision.CompareTag ("Spike")) {
            PlayerDamage(1);
        }
        else if (collision.CompareTag("Boss"))
        {
            PlayerDamage(2);
        }
        //else if (collision.CompareTag("Heart")) {
        //    playerCurrentHealth += 1;
        //}
    }

    void PlayerDamage(int damage) {
        playerCurrentHealth -= damage;
    }

    void PlayerLife(int life) {
        playerCurrentHealth += life;
    }
}

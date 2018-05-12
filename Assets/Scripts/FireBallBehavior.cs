using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallBehavior : MonoBehaviour {



    private GameObject player;
    //private PlayerController player;

    public GameObject fireBallImpactEffect;

    private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}

	//private void Awake()
	//{
 //       player = GameObject.FindGameObjectWithTag("Player");
 //       PlayerController playerComponents = player.GetComponent<PlayerController>();

 //       //if (!playerComponents.toRight)
 //       //{
 //       //    fireBallSpeed = -fireBallSpeed;
 //       //}
	//}

	// Update is called once per frame
	void Update () {
        

        player = GameObject.FindGameObjectWithTag("Player");
        PlayerController playerComponents = player.GetComponent<PlayerController>();

        if (playerComponents.fireBallDirection) {
            rb.velocity = new Vector2(playerComponents.fireBallSpeed, rb.velocity.y);
        }
        else {
            rb.velocity = new Vector2(-playerComponents.fireBallSpeed, rb.velocity.y);
        }

	}

	// destroy object when it collides with something
	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.CompareTag("Tilemap") || collision.CompareTag("Boss")) {
            Instantiate(fireBallImpactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
            Destroy(GameObject.FindWithTag("Projectile"));
        }
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomBehavior : MonoBehaviour {

    private Animator animator;
    private GameObject player;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        PlayerController playerComponents = player.GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    // bounce the player up if they jump on a mushroom
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            Debug.Log("Player enters mushroom trigger");
            animator.SetBool("Touched", true);

            player = GameObject.FindGameObjectWithTag("Player");
            PlayerController playerComponents = player.GetComponent<PlayerController>();

            playerComponents.bounce = 2f;
            playerComponents.bounceLength = .5f;
            playerComponents.bounceTimer = playerComponents.bounceLength;
        }
    }

    // stop playing animation when the player stops touching the mushroom
	private void OnTriggerExit2D(Collider2D collision)
	{
        if (collision.CompareTag("Player")) {
            Debug.Log("Player exits mushroom trigger");
            animator.SetBool("Touched", false);
        }
	}
}

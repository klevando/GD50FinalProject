using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeDamage : MonoBehaviour {

    private GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        PlayerController playerComponents = player.GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
        // knockback the player if they come in contact with spikes
        if (collision.CompareTag("Player")) {
            player = GameObject.FindGameObjectWithTag("Player");
            PlayerController playerComponents = player.GetComponent<PlayerController>();

            playerComponents.knockback = 5f;
            playerComponents.knockbackLength = .2f;
            playerComponents.knockbackTimer = playerComponents.knockbackLength;

            if (collision.transform.position.x < transform.position.x) {
                playerComponents.knockbackFromRight = false;
            }
            else {
                playerComponents.knockbackFromRight = true;
            }

        }
	}
}

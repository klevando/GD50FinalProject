using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    public Sprite[] heartArray;
    public Image heartsUI;
    public GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        PlayerController playerComponents = player.GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
        // show the current health of the player in hearts on the canvas
        player = GameObject.FindGameObjectWithTag("Player");
        PlayerController playerComponents = player.GetComponent<PlayerController>();
        heartsUI.sprite = heartArray[playerComponents.playerCurrentHealth];
	}
}

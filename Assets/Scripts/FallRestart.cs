using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FallRestart : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            int currentScene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentScene, LoadSceneMode.Single);
        }
    }
}

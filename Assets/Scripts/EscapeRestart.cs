using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeRestart : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // if escape is pressed reload the current scene
        if (Input.GetAxis("Cancel") == 1)
        {
            int currentScene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentScene, LoadSceneMode.Single);
        }
	}
}

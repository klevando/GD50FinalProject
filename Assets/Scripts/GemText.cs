using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class GemText : MonoBehaviour {

    //public List<GameObject> gemList = new List<GameObject>();
    public GameObject player;
    private int gems;
    public Text gemText;

	// Use this for initialization
	void Start () {
        gemText = GetComponent<Text>();
        SetGemText();
	}
	
	// Update is called once per frame
	void Update () {
        if (player != null) {
            gems = player.GetComponent<PlayerController>().gemTotal;
            SetGemText();
        }
	}

    void SetGemText () {
        gemText.text = "" + gems.ToString();
    }
}

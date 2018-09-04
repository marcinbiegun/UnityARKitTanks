using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance = null;

    // Use this for initialization
    void Start () {
        Application.targetFrameRate = 60;
        // Make self a publicly available singleton
        if (instance == null) { instance = this; } else if (instance != this) { Destroy(gameObject); }
        // Never destroy gameManager (on scene changes)
        DontDestroyOnLoad(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

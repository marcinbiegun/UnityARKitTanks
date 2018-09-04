using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance = null;
    public GameObject levelManagerPrefab;
    public GameObject levelManager;

    // Use this for initialization
    void Start () {
        Application.targetFrameRate = 60;
        // Make self a publicly available singleton
        if (instance == null) { instance = this; } else if (instance != this) { Destroy(gameObject); }
        // Never destroy gameManager (on scene changes)
        DontDestroyOnLoad(gameObject);

        levelManager = Instantiate(levelManagerPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

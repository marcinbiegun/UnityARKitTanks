using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncTransformToLevelManager : MonoBehaviour {

    private GameObject levelManager; 

	// Use this for initialization
	void Start () {

        if (levelManager == null) {
            var found = GameObject.Find("/LevelManager");
            if (found) {
                levelManager = found;
            }
        }

        if (levelManager) {
            levelManager.transform.SetParent(transform);
            //levelManager.transform.localScale = transform.localScale;
            //levelManager.transform.localPosition = transform.localPosition;
            //levelManager.transform.localRotation = transform.localRotation;
        }

    }

    // Update is called once per frame
    void Update () {
    }
}

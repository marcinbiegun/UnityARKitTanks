using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour {

    public float angle = 0f;
    public float power = 0f;
    public GameObject barrelObject;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        var newRotation = Quaternion.Euler(
            angle,
            barrelObject.transform.rotation.y,
            barrelObject.transform.rotation.z
        );
        barrelObject.transform.rotation = newRotation;
        var newLocalScale = new Vector3(
            barrelObject.transform.localScale.x,
            barrelObject.transform.localScale.y,
            1f + power
        );
        barrelObject.transform.localScale = newLocalScale;
    }
}

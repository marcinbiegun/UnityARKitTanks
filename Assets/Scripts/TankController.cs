using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour {

    public float rotation = 0f;
    public float angle = 0f;
    public float power = 0f;
    public float rotationDelta = 0f;
    public float angleDelta = 0f;
    public float powerDelta = 0f;
    public GameObject barrelObject;

    // Use this for initialization
    void Start () {
	}
	
	void Update () {
        // Rotate the tank's barrel
        barrelObject.transform.localRotation = Quaternion.Euler(
            angle + angleDelta,
            barrelObject.transform.rotation.y,
            barrelObject.transform.rotation.z
        );

        // Scale the tank's barrel
        barrelObject.transform.localScale = new Vector3(
            gameObject.transform.localScale.x,
            gameObject.transform.localScale.y,
            1f + power + powerDelta
        );

        // Rotate the tank
        gameObject.transform.localRotation = Quaternion.Euler(
            gameObject.transform.rotation.x,
            rotation + rotationDelta,
            gameObject.transform.rotation.z
        );

    }

    public void ApplyDeltas() {
        rotation += rotationDelta;
        angle += angleDelta;
        power += powerDelta;
        rotationDelta = 0f;
        angleDelta = 0f;
        rotationDelta = 0f;
    }

    public Transform ShotTransform() {
        var shotTransform = gameObject.transform;
        shotTransform.Rotate(angle + angleDelta, 0, 0);
        return shotTransform;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformPulse : MonoBehaviour {
    public Vector3 scale;
    public Vector3 position;
    public float speed = 1.0f;
    private Vector3 baseScale;
    private Vector3 basePosition;

    // Use this for initialization
    void Start () {
        baseScale = gameObject.transform.localScale;
        basePosition = gameObject.transform.localPosition;
    }

    // Update is called once per frame
    void Update () {
        float delta = Mathf.Sin(Time.time * speed);

        var newLocalScale = new Vector3(
            baseScale.x + (scale.x * delta),
            baseScale.y + (scale.y * delta),
            baseScale.z + (scale.z * delta)
        );
        gameObject.transform.localScale = newLocalScale;

        var newPosition = new Vector3(
            basePosition.x + (position.x * delta),
            basePosition.y + (position.y * delta),
            basePosition.z + (position.z * delta)
        );
        gameObject.transform.localPosition = newPosition;
    }
}

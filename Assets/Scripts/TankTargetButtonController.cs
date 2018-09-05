using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TankTargetButtonController : EventTrigger {

    private bool isPressed = false;
    private AudioSource audioSource;

    void Awake() {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = (AudioClip)Resources.Load("moving");
        audioSource.Stop();
    }

    // Update is called once per frame
    void Update () {
        if (isPressed) {
            var targetDelta = gameObject.GetComponent<TankTargetButton>().BuildTargetDelta();
            LevelManager.instance.AddTargetDelta(targetDelta);
        }
	}

    public override void OnPointerDown(PointerEventData eventData) {
        isPressed = true;
        audioSource.Play();
    }

    public override void OnPointerUp(PointerEventData eventData) {
        isPressed = false;
        audioSource.Stop();
        LevelManager.instance.ApplyTargetDelta();
    }

}

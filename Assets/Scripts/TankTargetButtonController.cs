using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TankTargetButtonController : EventTrigger {

    private bool isPressed = false;

	
	// Update is called once per frame
	void Update () {
        if (isPressed) {
            var targetDelta = gameObject.GetComponent<TankTargetButton>().BuildTargetDelta();
            LevelManager.instance.AddTargetDelta(targetDelta);
        }
	}

    public override void OnPointerDown(PointerEventData eventData) {
        isPressed = true;
    }

    public override void OnPointerUp(PointerEventData eventData) {
        isPressed = false;
        LevelManager.instance.ApplyTargetDelta();
    }

}

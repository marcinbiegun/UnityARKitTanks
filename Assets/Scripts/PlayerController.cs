using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private int controlMode = 0;
    private Vector3 initialMousePosition;
    private enum ControlMode { None, Rotation, Power };

    // Use this for initialization
    void Start () {
	}

    void Update() {
        if (Input.GetButtonDown("Fire1"))
            EnterRotationControl();
        if (Input.GetButtonDown("Fire2"))
            EnterPowerControl();
        if (Input.GetButtonUp("Fire1") || Input.GetButtonUp("Fire2")) {
            ReleaseControl();
        }
        if (Input.GetKeyDown("space"))
            GameManager.instance.Fire();
        if (Input.GetKeyDown("escape"))
            Application.Quit();

        PublishControlDeltas();
    }

    public void EnterRotationControl() {
        if (controlMode != (int)ControlMode.None) { return; }
        Debug.Log("Enetring ROTATION mode");

        controlMode = (int)ControlMode.Rotation;
        initialMousePosition = Input.mousePosition;
    }

    public void EnterPowerControl() {
        if (controlMode != (int)ControlMode.None) { return; }
        Debug.Log("Enetring POWER mode");

        controlMode = (int)ControlMode.Power;
        initialMousePosition = Input.mousePosition;
    }

    public void ReleaseControl() {
        Debug.Log("Enetring NONE mode");

        controlMode = 0;
        GameManager.instance.ApplyTankControlDelta();
    }

    void PublishControlDeltas() {
        if (controlMode == (int)ControlMode.Rotation) {
            Vector3 mouseDelta = Input.mousePosition - initialMousePosition;
            float rotation = mouseDelta.x / 10f;
            float angle = mouseDelta.y / 10f;
            GameManager.instance.SetTankControlDelta(rotation, angle, 0f);
        } else if (controlMode == (int)ControlMode.Power) {
            Vector3 mouseDelta = Input.mousePosition - initialMousePosition;
            float power = (mouseDelta.x + mouseDelta.y) / 100f;
            GameManager.instance.SetTankControlDelta(0f, 0f, power);
        }
    }


}

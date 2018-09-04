using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    enum ControlMode { None, Rotation, Power };
    private ControlMode controlMode = ControlMode.None;
    private Vector3 initialMousePosition;

    // Use this for initialization
    void Start () {
	}

    void Update_disabled() {
        if (Input.GetButtonDown("Fire1"))
            EnterRotationControl();
        if (Input.GetButtonDown("Fire2"))
            EnterPowerControl();
        if (Input.GetButtonUp("Fire1") || Input.GetButtonUp("Fire2")) {
            ReleaseControl();
        }
        if (Input.GetKeyDown("space"))
            LevelManager.instance.Fire();
        if (Input.GetKeyDown("escape"))
            Application.Quit();

        PublishControlDeltas();
    }

    public void EnterRotationControl() {
        if (controlMode != ControlMode.None) { return; }
        controlMode = ControlMode.Rotation;
        initialMousePosition = Input.mousePosition;
    }

    public void EnterPowerControl() {
        if (controlMode != ControlMode.None) { return; }
        controlMode = ControlMode.Power;
        initialMousePosition = Input.mousePosition;
    }

    public void ReleaseControl() {
        controlMode = ControlMode.None;
        LevelManager.instance.ApplyTargetDelta();
    }

    void PublishControlDeltas() {
        if (controlMode == ControlMode.None)
            return;

        Vector3 mouseDelta = Input.mousePosition - initialMousePosition;
        switch (controlMode)
        {
            case ControlMode.Rotation:
                float rotation = mouseDelta.x / 10f;
                float angle = mouseDelta.y / 10f;
                var rotationTargetDelta = new Tanks.Target(rotation, angle, 0f);
                LevelManager.instance.SetTargetDelta(rotationTargetDelta);
                break;
            case ControlMode.Power:
                float power = (mouseDelta.x + mouseDelta.y) / 100f;
                var powerTargetDelta = new Tanks.Target(0f, 0f, power);
                LevelManager.instance.SetTargetDelta(powerTargetDelta);
                break;
        }
    }


}

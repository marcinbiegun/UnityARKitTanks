using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TanksManager : MonoBehaviour {
    public GameObject tankPrefab;
    public GameObject projectilePrefab;
    private GameObject tank;

    private int controlMode = 0;
    private Vector3 initialMousePosition;
    private enum ControlMode { None, Rotation, Power };

    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        if (controlMode == (int)ControlMode.Rotation) {
            Vector3 mouseDelta = Input.mousePosition - initialMousePosition;
            var tankController = tank.GetComponent<TankController>();

            tankController.rotationDelta = mouseDelta.x / 10f;
            tankController.angleDelta = mouseDelta.y / 10f;
            if (Input.GetButtonDown("Fire1")) {
                ReleaseControlMode();
                tankController.ApplyDeltas();
            }
        }
    }

    public void CreateTankOnTerrain() {
        if (tank != null) {
            throw new UnityException("Tank is already spawned");
        }
        var tankPosition = GameManager.instance.terrainManager.RandomPosition();
        tank = Instantiate(tankPrefab, tankPosition, Quaternion.identity);
    }

    public void EnterRotationMode() {
        if (controlMode != (int)ControlMode.None) {
            return;
        }
        controlMode = (int)ControlMode.Rotation;
        initialMousePosition = Input.mousePosition;
    }

    public void ReleaseControlMode() {
        Debug.Log("Enetring NONE mode");
        controlMode = 0;
    }

    public void Fire() {
        var shotTransform = tank.GetComponent<TankController>().ShotTransform();
        float shotPower = tank.GetComponent<TankController>().power;

        GameObject newProjectile = Instantiate(projectilePrefab, shotTransform.position, shotTransform.rotation);
        newProjectile.GetComponent<Rigidbody>().AddForce(shotTransform.forward * shotPower * -1000f);
    }

}
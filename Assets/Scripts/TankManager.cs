using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankManager : MonoBehaviour {
    public GameObject tankPrefab;
    public GameObject projectilePrefab;
    private GameObject tank;
    private GameObject tankBarrel;

    public float rotation = 0f;
    public float angle = 0f;
    public float power = 0f;
    public float rotationDelta = 0f;
    public float angleDelta = 0f;
    public float powerDelta = 0f;


    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        DisplayAdjustmentsOnModel();
        DisplayAdjustmentsOnUI();
    }

    public void CreateTankOnTerrain() {
        if (tank != null) {
            throw new UnityException("Tank is already spawned");
        }
        var tankPosition = GameManager.instance.terrainManager.RandomPosition();
        tank = Instantiate(tankPrefab, tankPosition, Quaternion.identity);
        tankBarrel = tank.transform.Find("Barrel").gameObject;
        if (tankBarrel == null) {
            throw new UnityException("Unable to find tank's barrel");
        }
    }

    public void Fire() {
        var shotTransform = tankBarrel.transform;

        GameObject newProjectile = Instantiate(projectilePrefab, shotTransform.position, shotTransform.rotation);
        newProjectile.GetComponent<Rigidbody>().AddForce(shotTransform.forward * power * -1000f);
    }

    void DisplayAdjustmentsOnModel() {
        // Rotate the tank's barrel
        tankBarrel.transform.localRotation = Quaternion.Euler(
            angle + angleDelta,
            tankBarrel.transform.rotation.y,
            tankBarrel.transform.rotation.z
        );

        // Scale the tank's barrel
        tankBarrel.transform.localScale = new Vector3(
            tankBarrel.transform.localScale.x,
            tankBarrel.transform.localScale.y,
            1f + power + powerDelta
        );

        // Rotate the tank
        tank.transform.localRotation = Quaternion.Euler(
            tank.transform.rotation.x,
            rotation + rotationDelta,
            tank.transform.rotation.z
        );
    }

    void DisplayAdjustmentsOnUI() {
        GameManager.instance.uiManager.DisplayTankAdjustments(
            rotation + rotationDelta,
            angle + angleDelta,
            power + powerDelta
        );
    }

    public void SetControlDelta(float newRotationDelta, float newAngleDelta, float newPowerDelta) {
        rotationDelta = newRotationDelta;
        angleDelta = newAngleDelta;
        powerDelta = newPowerDelta;
    }

    public void ApplyControlDelta() {
        rotation += rotationDelta;
        angle += angleDelta;
        power += powerDelta;
        rotationDelta = 0f;
        angleDelta = 0f;
        powerDelta = 0f;
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankManager : MonoBehaviour {
    public GameObject tankPrefab;
    public GameObject projectilePrefab;
    private GameObject tank;
    private GameObject tankBarrel;

    public Tanks.Target target = new Tanks.Target(0f, 0f, 0f);
    public Tanks.Target targetDelta = new Tanks.Target(0f, 0f, 0f);

    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        DisplayTargetOnModel();
        DisplayTargetOnUI();
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
        newProjectile.GetComponent<Rigidbody>().AddForce(shotTransform.forward * target.power * -1000f);
    }

    void DisplayTargetOnModel() {
        // Rotate the tank's barrel
        tankBarrel.transform.localRotation = Quaternion.Euler(
            target.angle + targetDelta.angle,
            tankBarrel.transform.rotation.y,
            tankBarrel.transform.rotation.z
        );

        // Scale the tank's barrel
        tankBarrel.transform.localScale = new Vector3(
            tankBarrel.transform.localScale.x,
            tankBarrel.transform.localScale.y,
            1f + target.power + targetDelta.power
        );

        // Rotate the tank
        tank.transform.localRotation = Quaternion.Euler(
            tank.transform.rotation.x,
            target.rotation + targetDelta.rotation,
            tank.transform.rotation.z
        );
    }

    void DisplayTargetOnUI() {
        GameManager.instance.uiManager.DisplayTarget(target + targetDelta);
    }

    public void SetTargetDelta(Tanks.Target newTargetDelta) {
        targetDelta = newTargetDelta;
    }

    public void ApplyTargetDelta() {
        target += targetDelta;
        targetDelta = new Tanks.Target(0f, 0f, 0f);
    }

}
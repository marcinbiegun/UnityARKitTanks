﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankManager : MonoBehaviour {
    public Material tankMaterial;
    public GameObject tankPrefab;
    public GameObject projectilePrefab;
    private GameObject tank;
    private GameObject tankBarrel;
    private GameObject projectileSpawnPoint;

    public Tanks.Target target = new Tanks.Target(0f, 0f, 0.3f);
    public Tanks.Target targetDelta = new Tanks.Target(0f, 0f, 0f);

    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        DisplayTargetOnModel();
    }

    public void CreateTankOnTerrain(int tankId) {
        if (tank != null) {
            throw new UnityException("Tank is already spawned");
        }
        var tankPosition = LevelManager.instance.terrainManager.RandomPosition();
        tank = Instantiate(tankPrefab, tankPosition, Quaternion.identity);
        tank.GetComponent<TankController>().tankId = tankId;

        tankBarrel = tank.transform.Find("Barrel").gameObject;
        if (tankBarrel == null)
            throw new UnityException("Unable to find tank's barrel");

        projectileSpawnPoint = tank.transform.Find("Barrel/ProjectileSpawnPoint").gameObject;
        if (projectileSpawnPoint == null)
            throw new UnityException("Unable to find tank's projectile spawn point");

        ColorizeTank();
    }

    public void Fire() {
        var shotTransform = projectileSpawnPoint.transform;
        GameObject newProjectile = Instantiate(projectilePrefab, shotTransform.position, shotTransform.rotation);
        newProjectile.GetComponent<Rigidbody>().AddForce(shotTransform.forward * target.power * -1000f);
    }

    public void SetTargetDelta(Tanks.Target newTargetDelta) {
        targetDelta = newTargetDelta;
        DisplayTargetOnUI();
    }

    public void AddTargetDelta(Tanks.Target newTargetDelta) {
        targetDelta += newTargetDelta;
        DisplayTargetOnUI();
    }

    public void ApplyTargetDelta() {
        target += targetDelta;
        targetDelta = new Tanks.Target(0f, 0f, 0f);
    }

    public void Explode() {
        tank.GetComponent<BoxCollider>().enabled = false;
        foreach (Transform t in tank.GetComponentsInChildren<Transform>()) {
            Renderer rend = t.GetComponent<Renderer>();
            if (rend == null)
                continue;
            BoxCollider boxCollider = t.gameObject.AddComponent<BoxCollider>() as BoxCollider;
            Rigidbody rigidbody = t.gameObject.AddComponent<Rigidbody>() as Rigidbody;
        }
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
        LevelManager.instance.uiManager.DisplayTarget(target + targetDelta);
    }

    void ColorizeTank() {
        int colorizableLayer = LayerMask.NameToLayer("Colorizable");
        foreach (Transform t in tank.GetComponentsInChildren<Transform>()) {
            if (t.gameObject.layer != colorizableLayer)
                continue;
            Renderer rend = t.GetComponent<Renderer>();
            if (rend == null)
                continue;
            rend.material = tankMaterial;
        }
    }

}
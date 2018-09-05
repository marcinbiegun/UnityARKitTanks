using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankManager : MonoBehaviour {
    public Material tankMaterial;
    public GameObject tankPrefab;
    public GameObject projectilePrefab;
    private GameObject tank;
    private GameObject tankBarrel;
    private GameObject tankMarker;
    private GameObject projectileSpawnPoint;
    private bool fireLocked = false;

    public Tanks.Target target = new Tanks.Target(0f, 0f, 0.3f);
    public Tanks.Target targetDelta = new Tanks.Target(0f, 0f, 0f);

    private AudioSource audioSource;
    private AudioClip explosionClip;
    private AudioClip fireClip;

    private void Awake() {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.Stop();
        explosionClip = (AudioClip)Resources.Load("explosion");
        fireClip = (AudioClip)Resources.Load("shot");
    }

    void Update() {
        DisplayTargetOnModel();
    }

    public void CreateTankOnTerrain(int tankId, Vector3 tankPosition) {
        if (tank != null) {
            throw new UnityException("Tank is already spawned");
        }
        tank = Instantiate(tankPrefab, tankPosition, Quaternion.identity);
        tank.transform.SetParent(transform);
        tank.transform.localScale = new Vector3(1f, 1f, 1f);
        tank.GetComponent<TankController>().tankId = tankId;

        tankBarrel = tank.transform.Find("Barrel").gameObject;
        if (tankBarrel == null)
            throw new UnityException("Unable to find tank's barrel");

        projectileSpawnPoint = tank.transform.Find("Barrel/ProjectileSpawnPoint").gameObject;
        if (projectileSpawnPoint == null)
            throw new UnityException("Unable to find tank's projectile spawn point");

        tankMarker = tank.transform.Find("Marker").gameObject;
        if (tankMarker == null)
            throw new UnityException("Unable to find tank's marker");
        tankMarker.SetActive(false);

        ColorizeTank();
    }

    public void DestroyTank() {
        Destroy(tank);
        tank = null;
        tankBarrel = null;
        tankMarker = null;
        projectileSpawnPoint = null;
        fireLocked = false;
        target = new Tanks.Target(0f, 0f, 0f);
        targetDelta = new Tanks.Target(0f, 0f, 0f);
    }

    public void ShowMarker() {
        tankMarker.SetActive(true);
    }

    public void HideMarker() {
        tankMarker.SetActive(false);
    }

    public void Fire() {
        if (fireLocked)
            return;
        var shotTransform = projectileSpawnPoint.transform;
        GameObject newProjectile = Instantiate(projectilePrefab, shotTransform.position, shotTransform.rotation, transform);
        newProjectile.GetComponent<Rigidbody>().AddForce(shotTransform.forward * target.power * -1000f * transform.localScale.x);
        fireLocked = true;
        audioSource.clip = fireClip;
        audioSource.Play();
    }

    public void UnlockFire() {
        fireLocked = false;
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
        audioSource.clip = explosionClip;
        audioSource.Play();
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
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {

    private bool hasCollided = false;
    private AudioSource audioSource;

	// Use this for initialization
	void Start () {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.Stop();
        audioSource.clip = (AudioClip)Resources.Load("small_hit");
    }

    // Update is called once per frame
    void Update () {
		
	}

    void OnCollisionEnter(Collision col) {
        if (hasCollided)
            return;
        hasCollided = true;

        int terrainLAyer = LayerMask.NameToLayer("Terrain");
        int tankLayer = LayerMask.NameToLayer("Tank");
        int worldBorderLayer = LayerMask.NameToLayer("WorldBorder");

        if (col.gameObject.layer == terrainLAyer) {
            OnTerrainCollision();
        } else if (col.gameObject.layer == tankLayer) {
            OnTankCollision(col.gameObject);
        } else if (col.gameObject.layer == worldBorderLayer) {
            OnWorldBorderCollision();
        } else {
            OnTerrainCollision(); // Other projectiles, etc. do nothing
        }
    }

    void OnTerrainCollision() {
        audioSource.Play();
        LevelManager.instance.ProjectileMiss();
    }

    void OnTankCollision(GameObject tank) {
        int tankId = tank.GetComponent<TankController>().tankId;
        LevelManager.instance.ProjectileHitTank(tankId);
    }

    void OnWorldBorderCollision() {
        audioSource.Play();
        LevelManager.instance.ProjectileMiss();
        Destroy(gameObject);
    }
}

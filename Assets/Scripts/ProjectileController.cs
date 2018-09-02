using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {

    private bool hasCollided = false;

	// Use this for initialization
	void Start () {
		
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
        }
    }

    void OnTerrainCollision() {
        GameManager.instance.ProjectileMiss();
    }

    void OnTankCollision(GameObject tank) {
        int tankId = tank.GetComponent<TankController>().tankId;
        GameManager.instance.ProjectileHitTank(tankId);
    }

    void OnWorldBorderCollision() {
        GameManager.instance.ProjectileMiss();
        Destroy(gameObject);
    }
}

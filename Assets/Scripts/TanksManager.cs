using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TanksManager : MonoBehaviour {

    public GameObject tankPrefab;
    private GameObject tank;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void CreateTankOnTerrain() {
        var tankPosition = GameManager.instance.terrainManager.RandomPosition();
        tank = Instantiate(tankPrefab, tankPosition, Quaternion.identity);
    }
}

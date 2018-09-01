using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour {

    public GameObject terrain;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

    public Vector3 RandomPosition() {
        var terrainComp = terrain.GetComponent<Terrain>();
        var bounds = terrainComp.terrainData.bounds;
        float posX = Random.Range(bounds.min.x, bounds.max.x);
        float posZ = Random.Range(bounds.min.z, bounds.max.z);
        float posY = terrainComp.SampleHeight(new Vector3(posX, 0, posZ));
        return new Vector3(posX, posY, posZ);
    }
}

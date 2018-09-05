using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinTerrainManager : MonoBehaviour {

    public GameObject terrainPrefab;
    public GameObject terrain;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void CreateTerrain() {
        terrain = Instantiate(terrainPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
        terrain.transform.SetParent(transform);
        terrain.GetComponent<PerlinTerrain>().GeneratePerlinHeights(222f);

        // Center terrain around 0,0,0 coords
        var terrainSize = terrain.GetComponent<Terrain>().terrainData.size;
        terrain.transform.position = new Vector3(
            terrainSize.x * -0.5f,
            0f,
            terrainSize.z * -0.5f
        );
    }

    public Vector3 RandomPosition() {
        var terrainComp = terrain.GetComponent<Terrain>();
        var bounds = terrainComp.terrainData.bounds;
        float posX = Random.Range(bounds.min.x, bounds.max.x);
        float posZ = Random.Range(bounds.min.z, bounds.max.z);
        float posY = terrainComp.SampleHeight(new Vector3(posX, 0, posZ));

        return new Vector3(posX, posY, posZ) + terrain.transform.position;
    }
}

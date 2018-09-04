using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinTerrain : MonoBehaviour {

    public float noiseScale = 1.0f;
    public float noiseHeight = 1.0f;
    public float noiseSeed = 50.0f;

    public bool rebuild = false;
    private bool rebuildWas = false;

    // Use this for initialization
    void Start () {
        //GeneratePerlinHeights();
	}
	
	// Update is called once per frame
	void Update () {
        //if (rebuild != rebuildWas) {
        //    GeneratePerlinHeights();
        //}
        rebuildWas = rebuild;
	}

    public void GeneratePerlinHeights(float seed) {
        Debug.Log("Building terrain");

        float perlinSeedX = noiseSeed + seed;
        float perlinSeedY = -(noiseSeed + seed);

        var terrain = this.GetComponent<Terrain>();
        var width = terrain.terrainData.heightmapWidth;
        var height = terrain.terrainData.heightmapHeight;
        float[,] heights = new float[width, height];

        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                heights[x, y] = Mathf.PerlinNoise(
                    ((float)x / (float)width * noiseScale) + perlinSeedX,
                    ((float)y / (float)height * noiseScale) - perlinSeedY
                ) * noiseHeight;
            }
        }

        terrain.terrainData.SetHeights(0, 0, heights);
    }
}

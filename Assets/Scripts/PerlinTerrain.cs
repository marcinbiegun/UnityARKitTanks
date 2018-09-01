using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
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
        if (rebuild != rebuildWas) {
            GeneratePerlinHeights();
        }
        rebuildWas = rebuild;
	}

    void GeneratePerlinHeights() {
        Debug.Log("Building terrain");

        var terrain = this.GetComponent<Terrain>();
        var width = terrain.terrainData.heightmapWidth;
        var height = terrain.terrainData.heightmapHeight;
        float[,] heights = new float[width, height];

        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                heights[x, y] = Mathf.PerlinNoise(
                    ((float)x / (float)width * noiseScale) + noiseSeed,
                    ((float)y / (float)height * noiseScale) - noiseSeed
                ) * noiseHeight;
            }
        }

        terrain.terrainData.SetHeights(0, 0, heights);
    }
}

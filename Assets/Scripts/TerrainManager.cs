using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour {

    public GameObject terrainPrefab;
    public GameObject terrain;

    public void CreateTerrain(float scaleFactor) {
        var scale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
        terrain = Instantiate(terrainPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
        terrain.transform.localScale = scale;
        terrain.transform.SetParent(transform);
    }

    public Vector3 RandomPosition() {

        var terrainMesh = terrain.transform.GetChild(0);
        var meshFilter = terrainMesh.GetComponent<MeshFilter>().mesh;
        var bounds = meshFilter.bounds;
        var scaleX = terrainMesh.transform.localScale.x / transform.localScale.x;
        var scaleY = terrainMesh.transform.localScale.y / transform.localScale.y;
        var randomX = Random.Range(bounds.min.x / scaleX, bounds.max.x / scaleX);
        var randomZ = Random.Range(bounds.min.z / scaleY, bounds.max.z / scaleY);


        //Debug.Log("X is from " + bounds.min.x + " to " + bounds.max.x);
        //Debug.Log(randomZ);

        // FIXME layer not working, dunno why
        //var layerMask = LayerMask.NameToLayer("Terrain");

        RaycastHit hit;
        var rayStart = new Vector3(randomX, 10f, randomZ);

        var rayDir = Vector3.down;

        //if (Physics.Raycast(rayStart, rayDir, out hit, 1000f, layerMask)) {
        if (Physics.Raycast(rayStart, rayDir, out hit, 1000f)) {
            var randomPosition = hit.point;
            return randomPosition;
        } else {
            throw new UnityException("Failed to find a random point on the terrain");
        }


    }
}

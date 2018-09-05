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
        var randomX = Random.Range(bounds.min.x, bounds.max.x);
        var randomZ = Random.Range(bounds.min.z, bounds.max.z);

        //Debug.Log(randomX);
        //Debug.Log(randomZ);
        //randomX = 0.1f;
        //randomZ = -0.1f;

        // FIXME layer not working, dunno why
        //var layerMask = LayerMask.NameToLayer("Terrain");

        RaycastHit hit;
        var rayStart = new Vector3(randomX, 10f, randomZ);

        Debug.Log("random X is " + randomX);
        var rayDir = Vector3.down;

        Debug.DrawRay(rayStart, rayDir * 100f, Color.yellow);

        //if (Physics.Raycast(rayStart, rayDir, out hit, 1000f, layerMask)) {
        if (Physics.Raycast(rayStart, rayDir, out hit, 1000f)) {
            //    Debug.Log("Did Hit");
            var randomPosition = hit.point;
            Debug.Log("random position is " + randomPosition);
            return randomPosition;
        } else {
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not Hit");
            throw new UnityException("Failed to find a random point on the terrain");
        }


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public TankManager tankManager;
    public TerrainManager terrainManager;
    public UIManager uiManager;

    void Awake()
    {
        Application.targetFrameRate = 60;

        // Make self a publicly available singleton
        if (instance == null) { instance = this; }
        else if (instance != this) { Destroy(gameObject); }

        // Never destroy gameManager (on scene changes)
        DontDestroyOnLoad(gameObject);

        // Store reference to other manager intances
        tankManager = GetComponent<TankManager>();
        terrainManager = GetComponent<TerrainManager>();
        uiManager = GetComponent<UIManager>();

        // Let other managers do the setup
        Setup();
    }

    void Setup()
    {
        uiManager.Setup();
        tankManager.CreateTankOnTerrain();
    }

    public void DebugAction() {
        Debug.Log("Debug action!");
    }

    public void Fire() {
        tankManager.Fire();
    }

    public void SetTargetDelta(Tanks.Target targetDelta) {
        tankManager.SetTargetDelta(targetDelta);
    }

    public void ApplyTargetDelta() {
        tankManager.ApplyTargetDelta();
    }

    void Update()
    {

    }
}

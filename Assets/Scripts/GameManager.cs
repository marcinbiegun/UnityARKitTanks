using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public TankManager tank0Manager;
    public TankManager tank1Manager;
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
        terrainManager = GetComponent<TerrainManager>();
        uiManager = GetComponent<UIManager>();

        // Two tanks managers
        if (GetComponents<TankManager>().Length != 2) {
            throw new UnityException("Game<anager must have exactly 2 TankManagers");
        }
        tank0Manager = GetComponents<TankManager>()[0];
        tank1Manager = GetComponents<TankManager>()[1];

        // Let other managers do the setup
        Setup();
    }

    void Setup()
    {
        uiManager.Setup();
        tank0Manager.CreateTankOnTerrain();
        tank1Manager.CreateTankOnTerrain();
    }

    public void DebugAction() {
        Debug.Log("Debug action!");
    }

    public void Fire() {
        tank0Manager.Fire();
    }

    public void SetTargetDelta(Tanks.Target targetDelta) {
        tank0Manager.SetTargetDelta(targetDelta);
    }

    public void ApplyTargetDelta() {
        tank0Manager.ApplyTargetDelta();
    }

    void Update()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    public static LevelManager instance = null;
    public int activeTank;
    public TankManager tank0Manager;
    public TankManager tank1Manager;
    public TerrainManager terrainManager;
    public UIManager uiManager;

    void Awake() {
        Debug.Log("LEVEL MANAGE AWAKE - initalizing game");
        // Make self a publicly available singleton
        if (instance == null) { instance = this; } else if (instance != this) { Destroy(gameObject); }

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

    void Setup() {
        uiManager.Setup();
        uiManager.DisplayActiveTank(activeTank);
        terrainManager.CreateTerrain();
        tank0Manager.CreateTankOnTerrain(0, terrainManager.RandomPosition());
        tank1Manager.CreateTankOnTerrain(1, terrainManager.RandomPosition());
        SetActiveTank(0);
    }

    public void DebugAction() {
        uiManager.DisplayWinMessage(1);
        Debug.Log("Debug action!");
    }

    public void Fire() {
        if (ActiveTankManager())
            ActiveTankManager().Fire();
    }

    public void AddTargetDelta(Tanks.Target targetDelta) {
        if (ActiveTankManager())
            ActiveTankManager().AddTargetDelta(targetDelta);
    }

    public void SetTargetDelta(Tanks.Target targetDelta) {
        if (ActiveTankManager())
            ActiveTankManager().SetTargetDelta(targetDelta);
    }

    public void ApplyTargetDelta() {
        if (ActiveTankManager())
            ActiveTankManager().ApplyTargetDelta();
    }

    public void ProjectileMiss() {
        // Activate another tank
        int nexActiveTankId = activeTank == 0 ? 1 : 0;
        SetActiveTank(nexActiveTankId);
    }

    public void ProjectileHitTank(int tankId) {
        // Deactivate tank managers
        activeTank = -1;
        GetTankManager(tankId).Explode();
        int winnerTankId = tankId == 1 ? 0 : 1;
        uiManager.DisplayWinMessage(winnerTankId);
    }

    private void SetActiveTank(int tankId) {
        activeTank = tankId;
        tank0Manager.HideMarker();
        tank1Manager.HideMarker();
        ActiveTankManager().ShowMarker();
        ActiveTankManager().UnlockFire();
        uiManager.DisplayActiveTank(tankId);
    }

    TankManager ActiveTankManager() {
        if (activeTank == 0)
            return tank0Manager;
        if (activeTank == 1)
            return tank1Manager;
        return null;
    }

    TankManager GetTankManager(int tankId) {
        if (tankId == 0)
            return tank0Manager;
        if (tankId == 1)
            return tank1Manager;
        throw new UnityException("Bad tank id");
    }

}
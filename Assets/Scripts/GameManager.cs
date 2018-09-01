using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public TanksManager tanksManager;
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
        tanksManager = GetComponent<TanksManager>();
        terrainManager = GetComponent<TerrainManager>();
        uiManager = GetComponent<UIManager>();

        // Let other managers do the setup
        Setup();
    }

    void Setup()
    {
        uiManager.Setup();
    }

    public void DebugAction() {
        tanksManager.CreateTankOnTerrain();
    }

    public void ToggleTrackState(int index) {
    }

    public void ChangeStage(int index)
    {
    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
            Application.Quit();

    }
}

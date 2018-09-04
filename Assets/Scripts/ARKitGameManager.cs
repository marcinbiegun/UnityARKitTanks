using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARKitGameManager : MonoBehaviour {
    public static ARKitGameManager instance = null;
    public GameObject levelManagerPrefab;
    public GameObject levelManager;

    public GameUIManager gameUIManager;

    public enum GameMode { Calibration, Game };
    public GameMode gameMode;

    // Use this for initialization
    void Start() {
        Application.targetFrameRate = 60;
        // Make self a publicly available singleton
        if (instance == null) { instance = this; } else if (instance != this) { Destroy(gameObject); }
        // Never destroy gameManager (on scene changes)
        DontDestroyOnLoad(gameObject);

        //levelManager = Instantiate(levelManagerPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);

        // Set managers
        gameUIManager = GetComponent<GameUIManager>();
        gameUIManager.Setup();

        // Initialize level manager (the game)
        levelManager = Instantiate(levelManagerPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
        levelManager.name = "LevelManager";
        levelManager.transform.SetParent(transform.root);
        levelManager.GetComponent<UIManager>().SetCanvasScale(2.0f);

        // Go to calibration mode
        SetCalibrationMode();
    }

    // Update is called once per frame
    void Update() {
    }

    public void SetCalibrationMode() {
        Debug.Log("SetCalibraitonmode");
        gameMode = GameMode.Calibration;
        gameUIManager.SetDebugText("Calibration mode");
    }

    public void SetGameMode() {
        Debug.Log("Set GAME mode");
        gameMode = GameMode.Game;
        gameUIManager.SetDebugText("Game mode");
    }
}


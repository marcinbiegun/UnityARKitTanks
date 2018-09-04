using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

public class ARKitGameManager : MonoBehaviour {
    public static ARKitGameManager instance = null;
    public GameObject levelManagerPrefab;
    public GameObject levelManager;
    public UnityARGeneratePlane planesGenerator;

    public GameUIManager gameUIManager;

    public enum GameMode { Calibration, Game };
    public GameMode gameMode;

    private int planeIndex = 0;

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

        // Initialize planes generator
        planesGenerator = GetComponent<UnityARGeneratePlane>();

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

    public void ChangePlane() {
        Debug.Log("Change plane");
        var planes = planesGenerator.Planes();
        if (planes.Count == 0) {
            return;
        }

        planeIndex += 1;
        if (planeIndex >= planes.Count) {
            planeIndex = 0;
        }
        gameUIManager.SetDebugText("Plane " + planeIndex);

        int index = 0;
        foreach (var plane in planes) {
            if (index == planeIndex) {
                //levelManager.transform.SetParent(plane.gameObject.transform);
                levelManager.transform.position = plane.gameObject.transform.position;
                //levelManager.transform.rotation = plane.gameObject.transform.rotation;
                //levelManager.transform.localScale = plane.gameObject.transform.localScale;
                gameUIManager.SetDebugText("Plane SET " + planeIndex);
            }
            index += 1;
        }
    }


}


﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour {

    public Button calibrationButton;
    public Button gameButton;
    public Button changePlaneButton;
    public Text debugText;

    public void Setup() {
        calibrationButton.onClick.AddListener(ARKitGameManager.instance.SetCalibrationMode);
        gameButton.onClick.AddListener(ARKitGameManager.instance.SetGameMode);
        changePlaneButton.onClick.AddListener(ARKitGameManager.instance.ChangePlane);
    }

    public void SetDebugText(string text) {
        debugText.text = text;
    }
}

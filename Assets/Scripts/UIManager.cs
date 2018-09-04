using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public Canvas canvas;
    public Button debugButton;
    public Button fireButton;
    public Text rotationValue;
    public Text angleValue;
    public Text powerValue;
    public Text activeTankValue;
    public Text winValue;

    public void Setup() {
        debugButton.onClick.AddListener(LevelManager.instance.DebugAction);
        fireButton.onClick.AddListener(LevelManager.instance.Fire);
        winValue.gameObject.SetActive(false);
    }

    public void SetCanvasScale(float scale) {
        canvas.GetComponent<Canvas>().scaleFactor = scale;
    }

    public void DisplayTarget(Tanks.Target target) {
        rotationValue.text = target.rotation.ToString("0.0");
        angleValue.text = target.angle.ToString("0.0");
        powerValue.text = target.power.ToString("0.000");
    }

    public void DisplayActiveTank(int activeTank) {
        activeTankValue.text = activeTank.ToString();
    }

    public void DisplayWinMessage(int winnerId) {
        string message = "Player " + winnerId + " is the winner!";
        winValue.text = message;
        winValue.gameObject.SetActive(true);
    }
}
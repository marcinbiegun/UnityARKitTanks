using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button debugButton;
    public Text rotationValue;
    public Text angleValue;
    public Text powerValue;
    //public Button[] spawnerButtons;
    //public Dropdown stageDropdown;

    public void Setup()
    {
        debugButton.onClick.AddListener(GameManager.instance.DebugAction);
    }

    public void DisplayTankAdjustments(float rotation, float angle, float power) {
        rotationValue.text = rotation.ToString("0.0");
        angleValue.text = angle.ToString("0.0");
        powerValue.text = power.ToString("0.0");
    }

}
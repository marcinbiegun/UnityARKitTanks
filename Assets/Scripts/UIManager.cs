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

    public void Setup()
    {
        debugButton.onClick.AddListener(GameManager.instance.DebugAction);
    }

    public void DisplayTarget(Tanks.Target target) {
        rotationValue.text = target.rotation.ToString("0.0");
        angleValue.text = target.angle.ToString("0.0");
        powerValue.text = target.power.ToString("0.0");
    }

}
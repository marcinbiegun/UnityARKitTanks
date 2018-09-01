using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button debugButton;
    public Button rotationButton;
    public Button fireButton;
    //public Button[] spawnerButtons;
    //public Dropdown stageDropdown;

    public void Setup()
    {
        debugButton.onClick.AddListener(GameManager.instance.DebugAction);
        rotationButton.onClick.AddListener(GameManager.instance.Rotate);
        fireButton.onClick.AddListener(GameManager.instance.Fire);
    }

    //public void SetSpawnerButtonState(int index, bool newState)
    //{
    //spawnerButtons[index].colors = GetButtonColorBlock(newState);
    //}


    /*
    ColorBlock GetButtonColorBlock(bool state)
    {
        if (state)
        {
            return ColorBlock.defaultColorBlock;
        } else
        {
            var disabledColors = ColorBlock.defaultColorBlock;
            disabledColors.normalColor = Color.gray;
            disabledColors.highlightedColor = Color.gray;
            disabledColors.pressedColor = Color.gray;
            return disabledColors;
        }
    }
    */

}
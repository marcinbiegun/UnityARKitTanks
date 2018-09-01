using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button debugButton;
    //public Button[] spawnerButtons;
    //public Dropdown stageDropdown;

    public void Setup()
    {
        debugButton.onClick.AddListener(delegate {
            GameManager.instance.DebugAction();
        });
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
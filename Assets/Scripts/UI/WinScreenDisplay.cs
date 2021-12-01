using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class WinScreenDisplay : MonoBehaviour
{
    GameController _gController;
    public Text deathsText;
    public Text timeText;
    public Text totalTimeText;

    // Start is called before the first frame update
    void Start()
    {
        _gController = GameController.instance;

        if (deathsText != null) {
            deathsText.text = "Deaths: " + _gController.getDeaths();
        }
        if (timeText != null) {   
            timeText.text = "Level time: " + _gController.GetLevelTime(false);
        }
        if (totalTimeText != null) {   
            totalTimeText.text = "Total time: " + _gController.GetLevelTime(true);
        }
    }
}

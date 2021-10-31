using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Tracker : MonoBehaviour
{
    private GameController _gameController;
    private PlayerStats stats;

    public Slider rightSlider, leftSlider, upSlider, downSlider;
    public Text displayText;
    public Text powerText;
    public Text deathsText;
    
    void Start()
    {
        stats = PlayerStats.instance;
        _gameController = GameController.instance;
    }
    // Update is called once per frame
    void Update()
    {
        var horizontalCharge = stats.getHorizontalCharge();
        var verticalCharge = stats.getVerticalCharge();

        displayText.text = String.Format("Horizontal:{0:0.0} \nVertical:{1:0.0}", stats.getHorizontalCharge(), stats.getVerticalCharge());
        // can hide this if doesnt end up being implemented
        
        if (stats.getStunTime() > 0f) {
            powerText.text = String.Format("Stunned for: {0:0.0} seconds", stats.getStunTime());
        } else if (stats.isAttacking) {
            powerText.text = "Attacking";
        } else {
            powerText.text = "";
        }
        if (stats == null) {
            powerText.text = "Press P/Options (PS4 controller) to restart!";
        }


        rightSlider.value = Math.Max(0, horizontalCharge);
        leftSlider.value = Math.Min(0, horizontalCharge) * -1;
        upSlider.value = Math.Max(0, verticalCharge);
        downSlider.value = Math.Min(0, verticalCharge) * -1;

        deathsText.text = "Deaths: " + _gameController.getDeaths();
    }
}

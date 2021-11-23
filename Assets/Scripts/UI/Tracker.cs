using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Serialization;

public class Tracker : MonoBehaviour
{
    private GameController _gameController;
    private PlayerStats stats;

    public Slider rightSlider, leftSlider, upSlider, downSlider;
    public Text displayText;
    [FormerlySerializedAs("powerText")] public Text restartText;
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
        
        restartText.text = stats == null ? "Press Start/P to try again" : "";

        rightSlider.value = Math.Max(0, horizontalCharge);
        leftSlider.value = Math.Min(0, horizontalCharge) * -1;
        upSlider.value = Math.Max(0, verticalCharge);
        downSlider.value = Math.Min(0, verticalCharge) * -1;

        deathsText.text = "Deaths: " + _gameController.getDeaths();
    }
}

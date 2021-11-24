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
    public Slider upSecondarySlider, downSecondarySlider, leftSecondarySlider, rightSecondarySlider;
    public Text displayText;
    [FormerlySerializedAs("powerText")] public Text restartText;
    public Text deathsText;

    public float lerpScale = 5; // scale for background drain interpolation
    public float snapDistance = 0.3f; // snap the background to the meter location if within this distance
    public float drainDelay = 0.6f; // seconds between meter drain and background drain

    // private ParticleSystem upParticleSystem;

    void Start()
    {
        stats = PlayerStats.instance;
        _gameController = GameController.instance;
        // upParticles.SetActive(false);
        // upParticleSystem = upParticles.GetComponent<ParticleSystem>();
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

        if (Time.time - stats.lastUpAttackTime > drainDelay)
        {
            upSecondarySlider.value = Math.Abs(upSlider.value - upSecondarySlider.value) < snapDistance 
                ? upSlider.value 
                : Mathf.Lerp(upSecondarySlider.value, upSlider.value, lerpScale * Time.deltaTime);
        }
        if (Time.time - stats.lastDownAttackTime > drainDelay)
        {
            downSecondarySlider.value = Math.Abs(downSlider.value - downSecondarySlider.value) < snapDistance 
                ? downSlider.value 
                : Mathf.Lerp(downSecondarySlider.value, downSlider.value, lerpScale * Time.deltaTime);
        }
        if (Time.time - stats.lastLeftAttackTime > drainDelay)
        {
            leftSecondarySlider.value = Math.Abs(leftSlider.value - leftSecondarySlider.value) < snapDistance 
                ? leftSlider.value 
                : Mathf.Lerp(leftSecondarySlider.value, leftSlider.value, lerpScale * Time.deltaTime);
        }
        if (Time.time - stats.lastRightAttackTime > drainDelay)
        {
            rightSecondarySlider.value = Math.Abs(rightSlider.value - rightSecondarySlider.value) < snapDistance 
                ? rightSlider.value 
                : Mathf.Lerp(rightSecondarySlider.value, rightSlider.value, lerpScale * Time.deltaTime);
        }
        
    }
}

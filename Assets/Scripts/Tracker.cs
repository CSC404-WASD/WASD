using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Tracker : MonoBehaviour
{
    private PlayerStats stats;

    public Text displayText;
    public Text powerText;
    void Start()
    {
        stats = PlayerStats.instance;
    }
    // Update is called once per frame
    void Update()
    {

        displayText.text = String.Format("Horizontal:{0:0.0} \nVertical:{1:0.0}", stats.getHorizontalCharge(), stats.getVerticalCharge());
        // can hide this if doesnt end up being implemented
        
        if (stats.getStunTime() > 0f) {
            powerText.text = String.Format("Stunned for: {0:0.0} seconds", stats.getStunTime());
        } else if (stats.isAttacking) {
            powerText.text = "Attacking";
        } else {
            powerText.text = "";
        }
    }
}

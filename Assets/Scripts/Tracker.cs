using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Tracker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    float det = 0.0f;
    
    float wTime = 0.0f;
    float sTime = 0.0f;
    float aTime = 0.0f;
    float dTime = 0.0f;
    float sCharge = 0.0f;

    public Text displayText;
    public Text powerText;
    
    // Update is called once per frame
    void Update()
    {

        if(Input.GetAxisRaw("Vertical") > 0) {
            wTime += Time.deltaTime;
            sCharge = 0f;
        }
        else if(Input.GetAxisRaw("Vertical") < 0) {
            sTime += Time.deltaTime;
            sCharge += Time.deltaTime;
        }

        if(Input.GetAxisRaw("Horizontal") < 0) {
            aTime += Time.deltaTime;
        }
        if(Input.GetAxisRaw("Horizontal") > 0) {
            dTime += Time.deltaTime;
        }
        det = wTime*sTime - aTime*dTime;

        displayText.text = String.Format("W:{0:0.0} \nA:{1:0.0} \nS:{2:0.0} \nD:{3:0.0}", wTime, aTime, sTime, dTime);
        // can hide this if doesnt end up being implemented
        powerText.text = String.Format("Charge:{0:0.0}", sCharge);
    }
}

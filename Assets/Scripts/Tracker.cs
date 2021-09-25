using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public Text displayText;
    // Update is called once per frame
    void Update()
    {

        if(Input.GetAxisRaw("Vertical") > 0) {
            wTime += Time.deltaTime;
        }
        else if(Input.GetAxisRaw("Vertical") < 0) {
            sTime += Time.deltaTime;
        }

        if(Input.GetAxisRaw("Horizontal") < 0) {
            aTime += Time.deltaTime;
        }
        if(Input.GetAxisRaw("Horizontal") > 0) {
            dTime += Time.deltaTime;
        }
        det = wTime*sTime - aTime*dTime;

        displayText.text = "W: " + wTime.ToString()[0] + " A: " + sTime.ToString()[0] + " S: " + aTime.ToString()[0] + " D: " + dTime.ToString()[0];
    }
}

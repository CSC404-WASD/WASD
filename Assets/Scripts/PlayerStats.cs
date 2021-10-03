using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private static PlayerStats _instance;

    public static PlayerStats instance {get {return _instance;}}
    private float verCharge = 0.0f;
    private float horCharge = 0.0f;
    private bool stunned = false;
    private float stunTime = 0.0f;
    public bool isAttacking = false;

    private void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (stunned) {
            stunTime -= Time.deltaTime;
            if (stunTime <= 0) {
                stunned = false;
            }
            return;
        }
        if(Input.GetAxisRaw("Vertical") > 0) {
            verCharge += Time.deltaTime;
            //sCharge = 0f;
        }
        else if(Input.GetAxisRaw("Vertical") < 0) {
            verCharge -= Time.deltaTime;
            //sCharge += Time.deltaTime;
        }

        if(Input.GetAxisRaw("Horizontal") < 0) {
            horCharge -= Time.deltaTime;
        }
        if(Input.GetAxisRaw("Horizontal") > 0) {
            horCharge += Time.deltaTime;
        }

    }

    public void setStunned(bool stun = false, float time = 0.0f) {
        if(stun && time > 0.0f) {
            stunned = true;
            stunTime = time;
        } else {
            stunned = false;
        }
    }
    
    public bool isStunned() {
        return stunned;
    }

    public float getHorizontalCharge() {
        return horCharge;
    }

    public float getVerticalCharge() {
        return verCharge;
    }

    public void setVerticalDiff(float val) {
        verCharge += val;
    }
    
    public float getStunTime() {
        return stunTime;
    }
}

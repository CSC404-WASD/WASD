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
    public bool isDashing = false;
    public float maxVerCharge = 1.0f;
    public float maxHorCharge = 1.0f;

    public float meterChargeFactor = 1.0f;
    private Vector3 lastPosition;
    private float moveSpeed;

    private void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }

        lastPosition = this.transform.position;
        moveSpeed = GetComponent<PlayerMovement>().GetMoveSpeed();
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

        // Update meters
        if (!isDashing)
        {
            // Figure out change in position in camera frame.
            Vector3 deltaPositionC = Quaternion.Euler(0,-45, 0) * (this.transform.position - lastPosition);

            // Bars still use time as a unit (by calculating dist/speed), with optional factor.
            // This calculation makes moving diagonally build both meters slower than moving in one dir.
            verCharge += deltaPositionC.z / moveSpeed * meterChargeFactor;
            horCharge += deltaPositionC.x / moveSpeed * meterChargeFactor;
        }
        lastPosition = this.transform.position;

        // Check meter maximums
        if(verCharge > maxVerCharge)
        {
            verCharge = maxVerCharge;
        }
        if(verCharge < -maxVerCharge)
        {
            verCharge = -maxVerCharge;
        }
        if(horCharge > maxHorCharge)
        {
            horCharge = maxHorCharge;
        }
        if(horCharge < -maxHorCharge)
        {
            horCharge = -maxHorCharge;
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

    public void setHorizontalDiff(float val) {
        horCharge += val;
    }
    
    public float getStunTime() {
        return stunTime;
    }
}

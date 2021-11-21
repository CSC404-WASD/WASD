using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private static PlayerStats _instance;
    private static PlayerAudio _playerAudio;
    

    public static PlayerStats instance {get {return _instance;}}
    private float verCharge = 0.0f;
    private float horCharge = 0.0f;
    private bool stunned = false;
    private float stunTime = 0.0f;
    public bool isAttacking = false;
    public bool isDashing = false;
    public float maxVerCharge = 1.0f;
    public float maxHorCharge = 1.0f;
    
    public float leftChargeConsumption = 0.3f;
    public float rightChargeConsumption = 0.3f;
    public float downChargeConsumption = 1.5f;
    public float upChargeConsumption = 0.3f;

    public float meterChargeFactor = 1.0f;
    private Vector3 lastPosition;
    private float moveSpeed;
    
    public GameObject horizontalParticles;
    public GameObject verticalParticles;
    private static Color upParticleColor = Color.red;
    private static Color downParticleColor = Color.blue;
    private static Color leftParticleColor = Color.yellow;
    private static Color rightParticleColor = Color.green;

    public bool spellsCostMeter = true; // if false can use all spells infinitely for free

    private void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }

        lastPosition = this.transform.position;
        moveSpeed = GetComponent<PlayerMovement>().GetMoveSpeed();
        _playerAudio = GetComponent<PlayerAudio>();
        verticalParticles.SetActive(false);
        horizontalParticles.SetActive(false);
        
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

        var prevVerCharge = verCharge;
        var prevHorCharge = horCharge;

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
     
        IndicateChargeGain(prevVerCharge, prevHorCharge);
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

    private void IndicateChargeGain(float prevVerCharge, float prevHorCharge)
    {
        // up
        if (verCharge > 0)
        {
            var prevCharges = prevVerCharge > 0 ? Math.Floor(prevVerCharge / upChargeConsumption) : 0;
            var newCharges = Math.Floor(verCharge / upChargeConsumption);
            if (newCharges > prevCharges)
            {
                _playerAudio.PlayChargedSound("up", Convert.ToInt32(newCharges) - 1);
                var system = verticalParticles.GetComponent<ParticleSystem>();
                if (system != null)
                {
                    var main = system.main;
                    main.startColor = upParticleColor;
                }
                verticalParticles.SetActive(true);
            }

            if (newCharges == 0 && verticalParticles.active)
            {
                verticalParticles.SetActive(false);
            }
        }
        // down
        else
        {
            var prevCharges = prevVerCharge < 0 ? Math.Ceiling(prevVerCharge / downChargeConsumption) : 0;
            var newCharges = Math.Ceiling(verCharge / downChargeConsumption);
            if (newCharges < prevCharges) // negative
            {
                _playerAudio.PlayChargedSound("down", Math.Abs(Convert.ToInt32(newCharges)) - 1);
                
                var system = verticalParticles.GetComponent<ParticleSystem>();
                if (system != null)
                {
                    var main = system.main;
                    main.startColor = downParticleColor;
                }
                verticalParticles.SetActive(true);
            }
            
            if (newCharges == 0 && verticalParticles.active)
            {
                verticalParticles.SetActive(false);
            }
        }

        // right
        if (horCharge > 0)
        {
            var prevCharges = prevHorCharge > 0 ? Math.Floor(prevHorCharge / rightChargeConsumption) : 0;
            var newCharges = Math.Floor(horCharge / rightChargeConsumption);
            if (newCharges > prevCharges)
            {
                _playerAudio.PlayChargedSound("right", Convert.ToInt32(newCharges) - 1);
                var system = horizontalParticles.GetComponent<ParticleSystem>();
                if (system != null)
                {
                    var main = system.main;
                    main.startColor = rightParticleColor;
                }
                horizontalParticles.SetActive(true);
            }
            if (newCharges == 0 && horizontalParticles.active)
            {
                horizontalParticles.SetActive(false);
            }
        }
        // left
        else
        {
            var prevCharges = prevHorCharge < 0 ? Math.Ceiling(prevHorCharge / leftChargeConsumption) : 0;
            var newCharges = Math.Ceiling(horCharge / leftChargeConsumption);
            if (newCharges < prevCharges) // negative
            {
                _playerAudio.PlayChargedSound("left", Math.Abs(Convert.ToInt32(newCharges)) - 1);
                var system = horizontalParticles.GetComponent<ParticleSystem>();
                if (system != null)
                {
                    var main = system.main;
                    main.startColor = leftParticleColor;
                }
                horizontalParticles.SetActive(true);
            }
            
            if (newCharges == 0 && horizontalParticles.active)
            {
                horizontalParticles.SetActive(false);
            }
        }
    }
}

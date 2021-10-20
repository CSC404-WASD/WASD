using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    PlayerStats stats;

    public Rigidbody rigidbody;
    private PlayerAudio _playerAudio;
    
    public Transform attackPoint;
    public Vector3 attackRange = new Vector3(0.5f, 0.5f, 0.25f);
    //cube is just a visual for now, once animation is added can be removed
    public GameObject attackIndicator;
    public LayerMask enemyLayers;

    public float upChargeConsumption = 0.3f;
    public float upThreshold = 0.0f;
    public float upCooldown = 0.25f;
    float nextUpAttackTime = 0f;

    public float downChargeConsumption = 1.5f;
    public float downThreshold = 1.0f;
    public float downCooldown = 2.0f;
    float nextDownAttackTime = 0f;
    //object to spawn
    public GameObject downMine;

    public float leftChargeConsumption = 0.3f;
    public float leftThreshold = 0.0f;
    public float leftCooldown = 0.25f;
    public float dashCooldown = 1.0f;
    public float dashLength = 0.5f;
    float nextLeftTime = 0f;

    public float rightChargeConsumption = 0.3f;
    public float rightThreshold = 0f;
    public float rightCooldown = 0.25f;

    public float knockbackRadius = 1000f;
    public float knockbackPower = 10f;
    public float knockbackStun = 1f; // how long to stun enemy on knockback
    private float nextRightTime = 0f;

    void Start()
    {
        stats = PlayerStats.instance;
        rigidbody = GetComponent<Rigidbody>();
        _playerAudio = GetComponent<PlayerAudio>();
    }

    void Update()
    {
        
        //testing with ps4 controller on mac, button 6 = press left trigger button 7 = press right trigger
        // can also use axis 5 for a val between -1 and 1 (left trigger), or axis 6 for rt
        if (Input.GetKeyDown(KeyCode.U) && !stats.isAttacking && !stats.isDashing) {
            PerformUpAttack();
        // button 3 is triangle on ps (up) and y on xbox360 (up)
        } else if (Input.GetAxis("Vertical") > 0 && Input.GetKeyDown(KeyCode.JoystickButton3) && !stats.isAttacking && !stats.isDashing) {
            PerformUpAttack();
        } 
        //joystick button 1 = x (down) for ps4 controller, b (right) for xbox360 thanks devs
        if ((Input.GetKey(KeyCode.J) || Input.GetKeyDown(KeyCode.JoystickButton1)) && !stats.isDashing) {
            PerformDownAttack();
        }

        //button 2 is right (circle) on ps4 and left (x) on xbox360
        if ((Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.JoystickButton2)) && !stats.isAttacking && !stats.isDashing)
        {
            PerformDKnockback();
        }

        if (Input.GetKeyDown(KeyCode.H) && !stats.isDashing && !stats.isAttacking) {
            PerformADash();
        //button 0 is left on ps4(square) and down (a) on xbox360
        } else if (Input.GetAxis("Horizontal") < 0 && Input.GetKeyDown(KeyCode.JoystickButton0) && !stats.isDashing && !stats.isAttacking) {
            PerformADash();
        }
    }

    private void PerformUpAttack() {

        // check vertical charge
        float vCharge = stats.getVerticalCharge();

        // attack if cooldown refreshed and charge above threshold
        if (vCharge > upThreshold && nextUpAttackTime <= Time.time) {

            // if enough charge, subtract. else, set to 0 and stun
            if (vCharge >= upChargeConsumption) {
                stats.setVerticalDiff(-1f * upChargeConsumption);
            } 
            else {
                stats.setVerticalDiff(-1.0f * vCharge);
                stats.setStunned(true, upChargeConsumption - vCharge);
            }

            // execute attack
            stats.isAttacking = true;
            Collider[] hitColliders = Physics.OverlapBox(attackPoint.position, attackRange, Quaternion.identity, enemyLayers);
            attackIndicator.SetActive(true);
            foreach(Collider enemy in hitColliders) {
                //might want to make an Enemy file for this
                var enemyAI = enemy.GetComponent<BaseEnemyAI>();
                enemyAI.Die();
            }

            //delay next attack
            _playerAudio.PlayUpSound();
            nextUpAttackTime = Time.time + upCooldown;
            StartCoroutine(HideCube(0.25f));
        }
    }

    private void PerformDownAttack() {
        // check vertical charge and convert to positive (if in down) for easy use
        float vCharge = stats.getVerticalCharge() * -1;

        // attack if cooldown refreshed and charge above threshold
        if (vCharge > downThreshold && nextDownAttackTime <= Time.time) {

            // if enough charge, add consumption. else, set to 0 and stun
            if (vCharge >= downChargeConsumption) {
                stats.setVerticalDiff(downChargeConsumption);
            } 
            else {
                stats.setVerticalDiff(vCharge);
                stats.setStunned(true, downChargeConsumption - vCharge);
            }

            Instantiate(downMine, this.transform.position + new Vector3(1,0,1), Quaternion.identity);

            _playerAudio.PlayDownSound();
            //delay next attack
            nextDownAttackTime = Time.time + downCooldown;
        }
    }

    private void PerformADash() {
        // check horizontal charge
        float hCharge = stats.getHorizontalCharge();
        hCharge = -hCharge;

        // attack if cooldown refreshed and charge above threshold
        if (hCharge > leftThreshold && nextLeftTime <= Time.time) {

            // if enough charge, subtract. else, set to 0 and stun
            if (hCharge >=leftChargeConsumption) {
                stats.setHorizontalDiff(1f * leftChargeConsumption); // reversed
            } 
            else {
                stats.setVerticalDiff(1f * hCharge);
                stats.setStunned(true,leftChargeConsumption - hCharge); // reversed
            }

            // execute dash
            stats.isDashing = true;

            _playerAudio.PlayLeftSound();
            //delay next dash
            nextLeftTime = Time.time + leftCooldown;
            StartCoroutine(FinishDash(dashLength));
        }
    }
    
    private void PerformDKnockback()
    {
        var charge = stats.getHorizontalCharge();
        
        // charge above threshold and cooldown up
        if (charge <= rightThreshold || nextRightTime > Time.time)
        {
            return;
        }
            
        // stun if not enough charge
        
        if (charge < rightChargeConsumption)
        {
            stats.setStunned(true, rightChargeConsumption - charge);
            return;
        } else {
            stats.setHorizontalDiff(-1f * rightChargeConsumption); // reversed
        }
        
        // find entities to knock back
        var origin = rigidbody.position;
        var colliders = Physics.OverlapSphere(origin, knockbackRadius);
        stats.isAttacking = true;
        foreach (var other in colliders)
        {
            var collidedBody = other.GetComponent<Rigidbody>();
            if (collidedBody == null)
            {
                continue;
            }
            var direction = origin - other.transform.position;
            other.attachedRigidbody.velocity = -direction.normalized * knockbackPower;
            var enemyAI = other.GetComponent<BaseEnemyAI>();
            if (enemyAI != null)
            {
                enemyAI.stunForDuration(knockbackStun);
            }
        }
        
        _playerAudio.PlayRightSound();
        // delay next knockback
        nextRightTime = Time.time + rightCooldown;
        stats.isAttacking = false;
    }

    void OnDrawGizmosSelected() {
        if (attackPoint == null) {
            return;
        }
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPoint.position, attackRange);
    }

    IEnumerator HideCube(float time) {
        yield return new WaitForSeconds(time);
        attackIndicator.SetActive(false);
        stats.isAttacking = false;
    }

    IEnumerator FinishDash(float time) {
        yield return new WaitForSeconds(time);
        stats.isDashing = false;
    }
}

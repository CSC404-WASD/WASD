using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    PlayerStats stats;

    public Rigidbody rigidbody;
    
    public Transform attackPoint;
    public Vector3 attackRange = new Vector3(0.5f, 0.5f, 0.25f);
    //cube is just a visual for now, once animation is added can be removed
    public GameObject attackIndicator;
    public LayerMask enemyLayers;
    public float chargeConsumption = 0.3f;
    public float dChargeConsumption = 0.3f;
    public float wThreshold = 0.0f;
    public float dThreshold = 0f;
    public float wCooldown = 0.25f;
    public float dCooldown = 0.25f;

    public float knockbackRadius = 1000f;
    public float knockbackPower = 10f;
    public float knockbackStun = 1f; // how long to stun enemy on knockback

    float nextWAttackTime = 0f;
    private float nextDTime = 0f;
    void Start()
    {
        stats = PlayerStats.instance;
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //testing with ps4 controller on mac, button 6 = press left trigger button 7 = press right trigger
        // can also use axis 5 for a val between -1 and 1 (left trigger), or axis 6 for rt
        if (Input.GetKeyDown(KeyCode.U) && Input.GetKey(KeyCode.W)) {
            PerformWAttack();
        } else if (Input.GetAxis("Vertical") > 0 && Input.GetKeyDown(KeyCode.JoystickButton3)) {
            PerformWAttack();
        }


        if (Input.GetKeyDown(KeyCode.K) && !stats.isAttacking)
        {
            PerformDKnockback();
        }
    }

    private void PerformWAttack() {

        // check vertical charge
        float vCharge = stats.getVerticalCharge();

        // attack if cooldown refreshed and charge above threshold
        if (vCharge > wThreshold && nextWAttackTime <= Time.time) {

            // if enough charge, subtract. else, set to 0 and stun
            if (vCharge >= chargeConsumption) {
                stats.setVerticalDiff(-1f * chargeConsumption);
            } 
            else {
                stats.setVerticalDiff(-1.0f * vCharge);
                stats.setStunned(true, chargeConsumption - vCharge);
            }

            // execute attack
            stats.isAttacking = true;
            Collider[] hitColliders = Physics.OverlapBox(attackPoint.position, attackRange, Quaternion.identity, enemyLayers);
            attackIndicator.SetActive(true);
            foreach(Collider enemy in hitColliders) {
                Destroy(enemy.gameObject);
            }

            //delay next attack
            nextWAttackTime = Time.time + wCooldown;
            StartCoroutine(HideCube(0.25f));
        }
    }

    private void PerformDKnockback()
    {
        var charge = stats.getHorizontalCharge();
        
        // charge above threshold and cooldown up
        if (charge <= dThreshold || nextDTime > Time.time)
        {
            return;
        }
            
        // stun if not enough charge
        if (charge < dChargeConsumption)
        {
            stats.setStunned(true, dChargeConsumption = charge);
            return;
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
        
        // delay next knockback
        nextDTime = Time.time + dCooldown;
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


}

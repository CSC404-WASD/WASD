using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    PlayerStats stats;

    public Transform attackPoint;
    public Vector3 attackRange = new Vector3(0.5f, 0.5f, 0.25f);
    //cube is just a visual for now, once animation is added can be removed
    public GameObject attackIndicator;
    public LayerMask enemyLayers;
    public float chargeConsumption = 0.3f;
    public float wThreshold = 0.0f;
    public float wCooldown = 0.25f;

    float nextWAttackTime = 0f;
    void Start()
    {
        stats = PlayerStats.instance;
    }

    void Update()
    {
        //fix buttons for controller use later
        if (Input.GetKeyDown(KeyCode.U) && Input.GetKey(KeyCode.W)) {
            PerformWAttack();
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

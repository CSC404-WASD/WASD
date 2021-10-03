using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    PlayerStats stats;
    GameObject player;

    public Transform attackPoint;
    public Vector3 attackRange = new Vector3(0.5f, 0.5f, 0.25f);
    //cube is just a visual for now, once animation is added can be removed
    public GameObject cube;
    public LayerMask enemyLayers;
    public float chargeConsumption = 3.0f;

    float nextWAttackTime = 0f;
    void Start()
    {
        stats = PlayerStats.instance;
    }

    void Update()
    {
        //fix buttons for controller use later
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.W)) {
            PerformWAttack();
        }
    }

    private void PerformWAttack() {
        float vCharge = stats.getVerticalCharge();
        if (vCharge > 0f && nextWAttackTime <= Time.time) {
            if (vCharge >= chargeConsumption) {
                stats.setVerticalDiff(-1f * chargeConsumption);
            } else {
                stats.setVerticalDiff(-1.0f * vCharge);
                stats.setStunned(true, chargeConsumption - vCharge);
            }
            stats.isAttacking = true;

            Collider[] hitColliders = Physics.OverlapBox(attackPoint.position, attackRange, Quaternion.identity, enemyLayers);
            cube.SetActive(true);
            foreach(Collider enemy in hitColliders) {
                Destroy(enemy.gameObject);
            }
            //delay next attack
            nextWAttackTime = Time.time + 0.5f;
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
        cube.SetActive(false);
        stats.isAttacking = false;
    }


}

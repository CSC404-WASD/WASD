using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    //don't have a better idea on how to do this right now so just using a list so the
    // component can be reused
    public GameObject[] enemiesToTrigger;
    private bool activated = false;
    public void OnTriggerEnter(Collider other) {
        // original code
        if (!activated && other.gameObject.tag == "Player") {
            // var foundDynamicEnemyAis = FindObjectsOfType<DynamicEnemyAI>();
            // foreach (var e in foundDynamicEnemyAis) {
            //     e.ActivateEnemy(true);
            // }
            // activated = true;
            // Destroy(this.gameObject);
            foreach (var enemy in enemiesToTrigger) {
                var ai = enemy.GetComponent<BaseEnemyAI>();
                if (ai != null) {
                    ai.ActivateEnemy(true);
                }
            }
        }
    }
}

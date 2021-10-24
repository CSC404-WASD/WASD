using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    private bool activated = false;
    public void OnTriggerEnter(Collider other) {
        if (!activated && other.gameObject.tag == "Player") {
            var foundDynamicEnemyAis = FindObjectsOfType<DynamicEnemyAI>();
            foreach (var e in foundDynamicEnemyAis) {
                e.ActivateEnemy(true);
            }
            activated = true;
            Destroy(this.gameObject);
        }
    }
}

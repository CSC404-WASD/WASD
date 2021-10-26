using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomeEnemiesTrigger : MonoBehaviour
{
    private bool activated = false;
    public GameObject[] enemies;
    public void OnTriggerEnter(Collider other) {
        if (!activated && other.gameObject.tag == "Player") {
            foreach (GameObject enemy in enemies)
            {
                BaseEnemyAI ai = enemy.GetComponent<BaseEnemyAI>();
                ai.ActivateEnemy(true);
            }
            activated = true;
            Destroy(this.gameObject);
        }
    }
}

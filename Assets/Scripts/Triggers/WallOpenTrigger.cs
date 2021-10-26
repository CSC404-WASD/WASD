using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallOpenTrigger : MonoBehaviour
{

    public GameObject[] wallsToDestroy;

    public void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            var foundDynamicEnemyAis = FindObjectsOfType<DynamicEnemyAI>();
            foreach (var obj in wallsToDestroy) {
                Destroy(obj);
            }
            Destroy(this.gameObject);
        }
    }

}

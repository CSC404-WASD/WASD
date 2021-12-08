using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPassTrigger : MonoBehaviour
{  
    BoxCollider coll;

    void Start() {
        coll = gameObject.GetComponent<BoxCollider>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            var stats = other.gameObject.GetComponent<PlayerStats>();
            if (stats.isDashing) {
                SetPassable(true);
            }
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            var stats = other.gameObject.GetComponent<PlayerStats>();
            if (stats.isDashing) {
                SetPassable(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        SetPassable(false);
    }

    private void SetPassable(bool pass) {
        coll.isTrigger = pass;
    }
}

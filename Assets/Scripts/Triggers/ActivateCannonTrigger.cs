using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateCannonTrigger : MonoBehaviour
{
    public GameObject cannonParent;

    public void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            var parent = cannonParent.GetComponent<ActivatableObjectSpawner>();
            parent.ActivateSpawners(true);
            Destroy(this.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatableObjectSpawner : MonoBehaviour
{
    public bool spawnOnStart = false;
    void Start() {
        ActivateSpawners(spawnOnStart);
    }
    public void ActivateSpawners(bool activate) {
        var children = this.gameObject.transform.GetComponentsInChildren<ObstacleSpawner>();
        foreach(var child in children) {
            child.SetActive(activate);
        }
    }
}

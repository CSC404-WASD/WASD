using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyKillWallOpenTrigger : MonoBehaviour
{
    // public List<GameObject> enemiesBeforeDestroy;
    // public void open() {
    //     Destroy(this.gameObject);
    // }

    // public void removeEnemy(GameObject enemy) {
    //     if (enemiesBeforeDestroy.Exists(en => en == enemy)) {
    //         enemiesBeforeDestroy.Remove(enemy);
    //     }
    //     if (enemiesBeforeDestroy.Count == 0) {
    //         open();
    //     }
    // }
    public GameObject[] walls;
    private int count;

    void Start() {
        count = this.gameObject.transform.GetComponentsInChildren<BaseEnemyAI>().Length;
    }

    public void RemoveEnemy() {
        count -= 1;
        if (count <= 0) {
            BreakWalls();
        }
    }

    void BreakWalls() {
        foreach(var wall in walls) {
            Destroy(wall.gameObject);
        }
        Destroy(this.gameObject);
    }
}

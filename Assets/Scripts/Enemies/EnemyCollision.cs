using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    //ideally enemy kills itself on collision and not the mine
    private void OnCollisionEnter(Collision other)
    {
        // if (other.collider.CompareTag("Mine") )
        // {
        //     Destroy(this.gameObject);
        // }
        //Debug.Log("is this thing on");
    }
}

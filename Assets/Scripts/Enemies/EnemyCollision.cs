using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision other)
    {
        // if (other.collider.CompareTag("Mine") )
        // {
        //     Destroy(this.gameObject);
        // }
        Debug.Log("is this thing on");
    }
}

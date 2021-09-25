using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{   
    public void Start()
    {
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Obstacle"))
        {
            // put in losing state
            Destroy(this.gameObject);
        }
    }
}
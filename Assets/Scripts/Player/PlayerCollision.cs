using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{   
    PlayerStats stats;

    public void Start()
    {
        stats = PlayerStats.instance;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Enemy") || other.collider.CompareTag("Strong Enemy"))
        {
            // put in losing state
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile") && !stats.isDashing)
        {
            // put in losing state
            Destroy(this.gameObject);
        }
    }
}
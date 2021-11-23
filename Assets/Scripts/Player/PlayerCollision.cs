using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{   
    PlayerStats stats;
    GameController gController;
    private PlayerAudio _playerAudio;

    public void Start()
    {
        stats = PlayerStats.instance;
        gController = GameController.instance;
        _playerAudio = GetComponent<PlayerAudio>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Enemy") || other.collider.CompareTag("Strong Enemy"))
        {
            _playerAudio.PlayDeathSound();
            // put in losing state
            gController.KillPlayer();
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile") && !stats.isDashing)
        {
            _playerAudio.PlayDeathSound();
            // put in losing state
            gController.KillPlayer();
            Destroy(this.gameObject);
        }
    }
}
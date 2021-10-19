using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicEnemyAI : BaseEnemyAI
{
    private Vector3 velTowardPlayer;
    public float enemySpeed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    void FixedUpdate()
    {
        if (stunned)
        {
            return;
        }
        // Check if player is still alive
        if (player == null)
        {
            // Poll for player
            player = GameObject.FindWithTag("Player");
        }
        else
        {
            // Move toward player
            velTowardPlayer = (player.transform.position - this.transform.position).normalized * enemySpeed;
            velTowardPlayer.y = 0; // Don't fly
            GetComponent<Rigidbody>().MovePosition(this.transform.position + velTowardPlayer * Time.deltaTime);
        }
    }
}

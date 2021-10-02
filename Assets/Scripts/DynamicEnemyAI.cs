using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicEnemyAI : MonoBehaviour
{
    private GameObject player;
    private Rigidbody myRigidbody;
    private Vector3 unitVectTowardPlayer;
    public float walkImpulse = 1.0f;
    public float walkMaxSpeed = 8.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        myRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Check if player is still alive
        if (player == null)
        {
            // Poll for player
            player = GameObject.FindWithTag("Player");
        }
        else
        {
            // Calculate unit vect toward player
            unitVectTowardPlayer = (player.transform.position - this.transform.position).normalized;

            // Only walk if not already moving fast enough toward player
            if (myRigidbody.velocity.magnitude < walkMaxSpeed)
            {
                myRigidbody.AddForce(unitVectTowardPlayer * walkImpulse, ForceMode.VelocityChange);
            }
        }
    }
}

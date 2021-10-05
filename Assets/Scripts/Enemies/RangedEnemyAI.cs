using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyAI : MonoBehaviour
{
    private GameObject player;
    private Rigidbody myRigidbody;
    private Vector3 unitVectTowardPlayer;


    [Header("Movement")]
    public float walkImpulse = 8.0f;
    public float walkMaxSpeed = 8.0f;
    public float absMaxSpeed = 10.0f;

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

            // Get rid of the y component
            unitVectTowardPlayer.y = 0;
            unitVectTowardPlayer = unitVectTowardPlayer.normalized;

            // Only walk if not already moving fast enough toward player
            if (Vector3.Dot(myRigidbody.velocity, unitVectTowardPlayer) < walkMaxSpeed)
            {
                myRigidbody.velocity = new Vector3(unitVectTowardPlayer.x * walkImpulse, myRigidbody.velocity.y, unitVectTowardPlayer.z * walkImpulse);
            }
        }

        // Should not move faster than absMaxSpeed
        if(myRigidbody.velocity.magnitude <= absMaxSpeed)
        {
            myRigidbody.velocity = myRigidbody.velocity.normalized * absMaxSpeed;
        }
    }
}

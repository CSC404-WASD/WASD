using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyAI : BaseEnemyAI
{
    private Rigidbody myRigidbody;
    private ObstacleSpawner myProjectileSpawner;
    private Vector3 distTowardPlayer;


    [Header("Movement")]
    public float walkVelocity = 8.0f;
    public float walkMaxSpeed = 8.0f;
    public float absMaxSpeed = 10.0f;
    public float shootingRadius = 15.0f;
    public float shootingRadiusError = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        myRigidbody = GetComponent<Rigidbody>();
        myProjectileSpawner = GetComponent<ObstacleSpawner>();
        if (!active) {
            myProjectileSpawner.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        if (stunned || !active)
        {
            return;
        }
        Vector3 walkMovement = new Vector3(0,0,0);

        // Check if player is still alive
        if (player == null)
        {
            // Poll for player
            player = GameObject.FindWithTag("Player");
        }
        else
        {
            // Calculate dist toward player
            distTowardPlayer = (player.transform.position - this.transform.position);

            // Get rid of the y component
            distTowardPlayer.y = 0;

            Vector3 unitVectTowardPlayer = distTowardPlayer.normalized;
            //imported enemy model faces backwards for some reason so -1 here to flip it
            this.transform.forward = unitVectTowardPlayer * -1; // Rotate enemy


            // If dist is already in shooting radius, don't walk; shoot
            if (distTowardPlayer.magnitude > shootingRadius - shootingRadiusError && distTowardPlayer.magnitude < shootingRadius + shootingRadiusError)
            {
                myProjectileSpawner.SetActive(true);
            }
            // Otherwise, walk into shooting position
            else
            {
                myProjectileSpawner.SetActive(false);
                // Move toward player if far
                if(distTowardPlayer.magnitude > shootingRadius + shootingRadiusError)
                {
                    walkMovement.x = unitVectTowardPlayer.x * walkVelocity;
                    walkMovement.z = unitVectTowardPlayer.z * walkVelocity;
                }
                // Move away from player if close
                else
                {
                    walkMovement.x = -unitVectTowardPlayer.x * walkVelocity;
                    walkMovement.z = -unitVectTowardPlayer.z * walkVelocity;
                }
            }
        }

        // Execute movement
        myRigidbody.velocity = new Vector3(walkMovement.x, myRigidbody.velocity.y, walkMovement.z);

        // Should not move faster than absMaxSpeed
        if(myRigidbody.velocity.magnitude >= absMaxSpeed)
        {
            myRigidbody.velocity = myRigidbody.velocity.normalized * absMaxSpeed;
        }
    }
}

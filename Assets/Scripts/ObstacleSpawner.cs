using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject projectile;

    public float _spawnPeriod = 5;

    private float _nextSpawnTime;


    // Start is called before the first frame update
    void Start()
    {
        // Update variables
        _nextSpawnTime = Time.time + _spawnPeriod;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time >= _nextSpawnTime)
        {
            Spawn();
        }
    }

    void Spawn()
    {
        // Update variables
        _nextSpawnTime = Time.time + _spawnPeriod;

        //Debug.Log("Spawning object");

        Instantiate(projectile, this.transform.position, this.transform.rotation);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject projectile;

    public float _spawnPeriod = 5;

    private float _nextSpawnTime;
    private bool _bActive = true;

    public float rotationX = 0;
    public float rotationY = 0;
    public float rotationZ = 0;
    private Quaternion _shootOffset;


    // Start is called before the first frame update
    void Start()
    {
        // Update variables
        _nextSpawnTime = Time.time + _spawnPeriod;
        _shootOffset = Quaternion.Euler(rotationX, rotationY, rotationZ);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time >= _nextSpawnTime && _bActive)
        {
            Spawn();
        }
    }

    void Spawn()
    {
        // Update variables
        _nextSpawnTime = Time.time + _spawnPeriod;

        //Debug.Log("Spawning object");

        Instantiate(projectile, this.transform.position, _shootOffset * this.transform.rotation);
    }

    public void SetActive(bool bActive)
    {
        _bActive = bActive;
    }
}

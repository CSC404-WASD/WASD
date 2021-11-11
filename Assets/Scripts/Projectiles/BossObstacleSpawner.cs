using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossObstacleSpawner : MonoBehaviour
{
    public GameObject projectile;
    public Transform target;

    public float _spawnPeriod = 5;

    private float _nextSpawnTime;
    private bool _bActive = true;

    public float rotationX = 0;
    public float rotationY = 0;
    public float rotationZ = 0;
    private Quaternion _shootOffset;

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
            //transform.LookAt(target, Vector3.up);
            // Vector3 targetDirection = target.position - transform.position;
            // targetDirection.y = 0.0f;
            // float singleStep = 1.0f * Time.deltaTime;
            // Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, 3.0f, 3.0f);
            // transform.rotation = Quaternion.LookRotation(newDirection);
            Spawn();
        }
    }

    void Update() {
        if (target != null) {
            Vector3 targetDirection = target.position - transform.position;
            targetDirection.y = 0.0f;
            float singleStep = 1.0f * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 1.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }

    void Spawn()
    {
        // Update variables
        _nextSpawnTime = Time.time + _spawnPeriod;

        for (int i = 0; i < 4; i++) {
            _shootOffset = Quaternion.Euler(rotationX, 90 * i, rotationZ);
            Instantiate(projectile, this.transform.position, _shootOffset * this.transform.rotation);
        }
    }

        public void SetActive(bool bActive)
    {
        _bActive = bActive;
    }
}

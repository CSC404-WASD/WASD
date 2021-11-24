using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject projectile;
    ProjectileAudio _pAudio;

    public float _spawnPeriod = 5;

    private float _nextSpawnTime;
    private bool _bActive = true;

    public Vector3 offset = new Vector3(0,0,0);

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
        _pAudio = GetComponent<ProjectileAudio>();
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

        Instantiate(projectile, this.transform.position + offset, _shootOffset * this.transform.rotation);
        if (_pAudio != null) {
            _pAudio.PlayShootClip();
        }
    }

    public void SetActive(bool bActive)
    {
        _bActive = bActive;
    }
}

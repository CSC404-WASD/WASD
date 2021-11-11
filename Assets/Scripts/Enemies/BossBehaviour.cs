using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class BossBehaviour : MonoBehaviour
{
    public GameObject downEnemy;
    public GameObject upEnemy;

    public GameObject rangedEnemy;
    
    public GameObject rightEnemy;
    public GameObject leftEnemy;
    public Vector3 rightSpawnPosition;
    
    public Material activateMaterial;
    public float spawnInterval;

    public Color[] indicatorColours;

    private int _attackIndex = 0;

    Color colourStart = Color.black;
    Renderer rend;

    private float _nextSpawnTime;
    private bool _bActive = true;

    public float spawnDistance;

    void Start()
    {
        // Update variables
        _nextSpawnTime = Time.time + spawnInterval;
        GetComponent<Renderer>().material = activateMaterial;
        rend = GetComponent<Renderer>();
    }

    void FixedUpdate()
    {
        if (Time.time >= _nextSpawnTime && _bActive)
        {

            if (_attackIndex == 0) {
                DownSpawn();
            } else if (_attackIndex == 1) {
                UpSpawn();
            } else if (_attackIndex == 2) {
                RightSpawn();
            } else if (_attackIndex == 3) {
                LeftSpawn();
            }
            _nextSpawnTime = Time.time + spawnInterval;
            
            _attackIndex++;
            if (_attackIndex >= indicatorColours.Length) {
                StartCoroutine(SetBossVulnerable(10.0f));
            }
        } else if (_attackIndex < indicatorColours.Length) {
            //also Mathf.PingPong(Time.time, duration) for back and forth
            float lerp = Time.time % spawnInterval / spawnInterval;
            rend.material.color = Color.Lerp(colourStart, indicatorColours[_attackIndex], lerp);
        }

        
    }

    public void DownSpawn() {
        Vector3 middlePosition = transform.position + new Vector3(-30,0,-30);

        for (int i = -1; i < 2; i++) {
            Vector3 spawnPos = middlePosition + new Vector3(i * 5,0 ,i * 5);
            Instantiate(downEnemy, spawnPos, transform.rotation);
        }

        Vector3 spawnPos2 = middlePosition + new Vector3(-5,0,5);
        Instantiate(rangedEnemy, spawnPos2, transform.rotation);
        spawnPos2 = middlePosition + new Vector3(5,0,-5);
        Instantiate(rangedEnemy, spawnPos2, transform.rotation);
    }

    public void UpSpawn() {
        Vector3 middlePosition = transform.position + new Vector3(30,0,30);

        //spawn two guys
        for (int i = -1; i < 2; i+=2) {
            Vector3 spawnPos = middlePosition + new Vector3(i * 2.5f,0 ,-i * 2.5f);
            Instantiate(upEnemy, spawnPos, transform.rotation);
        }
    }

    public void RightSpawn() {
        StartCoroutine(SpawnLaser(0.1f, 5, rightEnemy.transform.rotation, rightSpawnPosition, new Vector3(0,0,20)));
        StartCoroutine(SpawnLaserX(2.0f));
    }

    IEnumerator SpawnLaser(float time, int count, Quaternion rot, Vector3 position, Vector3 posDif) {
        yield return new WaitForSeconds(time);
        Instantiate(rightEnemy, position, rot);
        position += posDif;
        if (count > 0) {
            StartCoroutine(SpawnLaser(time, count - 1, rot, position, posDif));
        } 
    }

    IEnumerator SpawnLaserX(float time) {
        yield return new WaitForSeconds(time);
        Quaternion rot = Quaternion.Euler(new Vector3(90, 0, 0));
        StartCoroutine(SpawnLaser(0.1f, 5, rot, new Vector3(10, 0, 65), new Vector3(20,0,0)));
    }

    public void LeftSpawn() {
        Vector3 middlePosition = transform.position + new Vector3(-30,0,30);

        //spawn two guys
        for (int i = -1; i < 2; i+=2) {
            Vector3 spawnPos = middlePosition + new Vector3(i * 2.5f,0 ,i * 2.5f);
            Instantiate(leftEnemy, spawnPos, transform.rotation);
        }
    }

    IEnumerator SetBossVulnerable(float time) {
        yield return new WaitForSeconds(time);
        rend.material.color = Color.white;
        this.GetComponent<BossEnemyAI>().SetVulnerable();
    }
}

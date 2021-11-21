using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{
    public float moveSpeed = 0.2f;
    public float lifetime = 15.0f;
    private float spawnTime;

    void Start()
    {
        spawnTime = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 movement = new Vector3(0, 0, moveSpeed) * Time.deltaTime;
        transform.Translate(movement);

        if (Time.time - spawnTime > lifetime)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Enemy") && !other.CompareTag("Cannon") && !other.CompareTag("Strong Enemy"))
        {
            Destroy(this.gameObject);
        }
    }
}

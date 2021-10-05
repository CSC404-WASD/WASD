using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{
    public float moveSpeed = 0.2f;

    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 movement = new Vector3(0, 0, moveSpeed);
        transform.Translate(movement);
    }

    private void OnCollisionEnter(Collision other)
    {
        if(!other.collider.CompareTag("Enemy") && !other.collider.CompareTag("Cannon"))
        {
            Destroy(this.gameObject);
        }
    }
}

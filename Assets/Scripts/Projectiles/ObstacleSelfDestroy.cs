using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSelfDestroy : MonoBehaviour
{
    public float destroyAfter;
    private float _startTime;

    void Start() {
        _startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > _startTime + destroyAfter) {
            Destroy(this.gameObject);
        }
    }
}

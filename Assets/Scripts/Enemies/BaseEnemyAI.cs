using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyAI : MonoBehaviour
{
    protected GameObject player;

    protected bool stunned = false;
    private float stunTime = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (stunned)
        {
            stunTime -= Time.deltaTime;
            print("stunned " + stunTime);
            if (stunTime <= 0)
            {
                print("done stun");
                stunned = false;
            }
        }
        else
        {
            print("Not stunned");
        }
    }

    // stun for at least duration
    public void stunForDuration(float duration)
    {
        stunned = true;
        stunTime = duration;
    }
}

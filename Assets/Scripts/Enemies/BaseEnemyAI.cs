using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyAI : MonoBehaviour
{
    EnemyController eController;
    protected GameObject player;

    protected bool stunned = false;
    private float stunTime = 0f;
    public bool active = true;
    
    // Start is called before the first frame update
    public void Start()
    {
        //can also move this logic to a constructor (doesn't work with current 
        // singleton) or perhaps not the AI file
        eController = EnemyController.instance;
        eController.addEnemy();
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (stunned)
        {
            stunTime -= Time.deltaTime;
            if (stunTime <= 0)
            {
                this.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
                stunned = false;
            }
        }
    }

    // stun for at least duration
    public void stunForDuration(float duration)
    {
        stunned = true;
        stunTime = duration;
    }

    //Die and remove the enemy from the controller
    public virtual void Die() {
        var parent = this.gameObject.GetComponentInParent<EnemyKillWallOpenTrigger>();
        if (parent != null) {
            parent.RemoveEnemy();
        }
        Destroy(this.gameObject);
        eController.removeEnemy();
    }

    public void ActivateEnemy(bool param) {
        active = param;
    }
}

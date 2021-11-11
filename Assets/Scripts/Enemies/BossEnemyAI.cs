using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyAI : BaseEnemyAI
{
    GameController _gController;
    // Start is called before the first frame update
    void Start()
    {
        this.active = false;
        _gController = GameController.instance;
    }

    public void SetVulnerable() {
        this.active = true;
    }

    override public void Die() {
        if (!this.active) {
            return;
        }
        Destroy(this.gameObject);
        _gController.WinBossLevel(); 
    }  
}

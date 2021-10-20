using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    GameController gameController;

    private static EnemyController _instance;
    public static EnemyController instance {get {return _instance;}}
    private int numEnemies = 0;

    void Start()
    {
        gameController = GameController.instance;
    }

    private void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    public void addEnemy() {
        numEnemies++;
    }

    public void removeEnemy() {
        numEnemies--;
        //can open a door probably here
        if (numEnemies == 0) {
            gameController.WinGame();
        }
    }    
}

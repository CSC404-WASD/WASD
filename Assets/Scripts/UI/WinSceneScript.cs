using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinSceneScript : MonoBehaviour
{

    private GameController _gameController;

    public Text deathsText;
    
    void Start()
    {
        _gameController = GameController.instance;
        var deaths = _gameController.getDeaths();
        deathsText.text = deaths == 1 ? "1 death" : (deaths + " deaths");
    }
}

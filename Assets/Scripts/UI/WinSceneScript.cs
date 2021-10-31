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
        deathsText.text = _gameController.getDeaths() + " deaths";
    }
}

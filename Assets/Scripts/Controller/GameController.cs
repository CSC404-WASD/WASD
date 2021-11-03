using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    ControllerLayouts cLayout; 

    private static GameController _instance;
    public static GameController instance {get {return _instance;}}

    private static int deaths = 0;

    private void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    void Start() {
        cLayout = ControllerLayouts.instance;
        if (cLayout == null) // If you are opening scenes from outside the menu. Debug.
        {
            cLayout = this.gameObject.AddComponent(typeof(ControllerLayouts)) as ControllerLayouts;
            cLayout.setLayout(ControllerType.XBOX360);
        }
    }

    public void WinLevel() {
        var levelList = FindObjectOfType<LevelSwitchController>();
        if (levelList != null) {
            levelList.NextLevel();
            if (levelList.NoMoreLevels() || !levelList.onLevelSequence) {
                LoadScene("WinScene");
            } else{
                LoadScene("InBetweenLevelMenu");
            }
        } else {
            LoadScene("WinScene");
        }
    }

    private void LoadScene(string level)
    {
        var curLevel = SceneManager.GetActiveScene().name;
        if (level != "WinScene" && curLevel != level)
        {
            deaths = 0;
        }
        SceneManager.LoadScene(level);
    }

    public void RestartGame() {
        //probably better way of doing this
        Scene scene = SceneManager.GetActiveScene();
        LoadScene(scene.name);
        deaths++;
    }

    void Update() {
        //button 9 is options on ps4 (right side), button 8 is share (left hand side)
        //on xbox360, 8 is left analog pressed 9 is right analog pressed (again bad mapping)
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(cLayout.restartButton())) {
            RestartGame();
        } else if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(cLayout.pauseButton())) {
            LoadScene("MainMenuScene");
            var levelList = FindObjectOfType<LevelSwitchController>();
            if (levelList != null) {
                levelList.ResetLevelList();
            }
        }
    }

    public int getDeaths()
    {
        return deaths;
    }
}

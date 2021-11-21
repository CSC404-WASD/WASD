using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    ControllerLayouts cLayout; 

    private static GameController _instance;
    public GameObject canvas;
    private GameObject _pauseUI;
    private GameObject _nonPauseUI;
    public static GameController instance {get {return _instance;}}

    private static int deaths = 0;
    private bool _isPaused = false;
    private bool _isDead = false;

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
        if (canvas != null) {
            _pauseUI = canvas.transform.Find("PauseUI").gameObject;
            _nonPauseUI = canvas.transform.Find("NonPauseUI").gameObject;
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

    public void LoadScene(string level)
    {
        var curLevel = SceneManager.GetActiveScene().name;
        if (level != "WinScene" && curLevel != level)
        {
            deaths = 0;
        }
        ResetPause();
        SceneManager.LoadScene(level);
        if (level == "MainMenuScene") {
            var levelList = FindObjectOfType<LevelSwitchController>();
            if (levelList != null) {
                levelList.ResetLevelList();
            }
        }
    }

    public void RestartGame() {
        //probably better way of doing this
        Scene scene = SceneManager.GetActiveScene();
        GameObject player = GameObject.Find("Player Bot");
        if (!_isDead) {
            Destroy(player);
        }
        ResetPause();
        LoadScene(scene.name);
    }

    void Update() {

        //button 9 is options on ps4 (right side), button 8 is share (left hand side)
        //on xbox360, 8 is left analog pressed 9 is right analog pressed (again bad mapping)
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(cLayout.restartButton())) {
            //RestartGame();
            Scene scene = SceneManager.GetActiveScene();
            if (scene.name != "WinScene") {
                if (_isDead) {
                    RestartGame();
                } else {
                    TogglePause();
                }
            }
        } else if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(cLayout.pauseButton())) {
            Scene scene = SceneManager.GetActiveScene();
            if (scene.name == "WinScene") {
                LoadScene("MainMenuScene");
                var levelList = FindObjectOfType<LevelSwitchController>();
                if (levelList != null) {
                    levelList.ResetLevelList();
                }
            }

        }
    }

    public void TogglePause() {
        if (_isPaused) {
            Time.timeScale = 1;
            _pauseUI.SetActive(false);
            _nonPauseUI.SetActive(true);
        } else {
            Time.timeScale = 0;
            _pauseUI.SetActive(true);
            _nonPauseUI.SetActive(false);
        }
        _isPaused = !_isPaused;
    }

    public int getDeaths()
    {
        return deaths;
    }

    public void ResetPause() {
        Time.timeScale = 1;
        if (canvas != null) {
            _pauseUI.SetActive(false);
            _nonPauseUI.SetActive(true);
        }
        _isPaused = false;
        _isDead = false;
    }

    public void KillPlayer() {
        deaths++;
        _isDead = true;
    }
}

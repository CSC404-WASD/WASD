using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private static GameController _instance;
    public static GameController instance {get {return _instance;}}

    private void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    public void WinGame() {
        LoadScene("WinScene");
    }

    private void LoadScene(string level) {
        SceneManager.LoadScene(level);
    }

    public void RestartGame() {
        //probably better way of doing this
        Scene scene = SceneManager.GetActiveScene();
        LoadScene(scene.name);
    }

    void Update() {
        //button 9 is options on ps4 (right side), button 8 is share (left hand side)
        //on xbox360, 8 is left analog pressed 9 is right analog pressed (again bad mapping)
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.JoystickButton9)) {
            RestartGame();
        } else if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton8)) {
            Application.Quit();
        }
    }
}

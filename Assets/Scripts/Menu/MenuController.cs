using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MenuTypes;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private int currentOption;
    public GameObject[] currentOptions;
    private int lastAxis = 0;

    void Update() {
        if (Input.GetAxis("Vertical") < 0 && lastAxis > -1) {
            MoveCursorDown();
        } else if (Input.GetAxis("Vertical") > 0 && lastAxis < 1) {
            MoveCursorUp();
        }

        if (Input.GetAxis("Vertical") > -0.01 && Input.GetAxis("Vertical") < 0.01) {
            lastAxis = 0;
        }

        //joystick button 1 = x (down) for ps4 controller, b (right) for xbox360 thanks devs
        if (Input.GetKey(KeyCode.Return) || Input.GetKeyDown(KeyCode.JoystickButton1)) {
            ClickMenuOption();
        }
    }

    void MoveCursorDown() {
        Debug.Log("down");
        if (currentOption == null && currentOptions.Length > 1) {
            currentOption = 1;
        } else if (currentOption < currentOptions.Length - 1) {
            UpdateHighlight(false);
            currentOption++;
        }
        UpdateHighlight(true);
        lastAxis = -1;
    }

    void MoveCursorUp() {
        Debug.Log("up");
        if (currentOption == null) {
            currentOption = 0;
        } else if (currentOption > 0) {
            UpdateHighlight(false);
            currentOption--;
        }
        UpdateHighlight(true);
        lastAxis = 1;
    }

    void UpdateHighlight(bool show) {
        if (show) {
            currentOptions[currentOption].GetComponent<Text>().color = new Color(255,0,0,1);
        } else {
            currentOptions[currentOption].GetComponent<Text>().color = new Color(0,0,0,1);
        }
    }

    void ClickMenuOption() {
        var menuOptionData = currentOptions[currentOption].GetComponent<MenuOptionData>();
        if (menuOptionData.menuType == MenuType.Load) {
            LoadScene(menuOptionData.levelName);
        } else if (menuOptionData.menuType == MenuType.Exit) {
            Application.Quit();
        } else if (menuOptionData.menuType == MenuType.Container) {
            //do something with children ideally
        } else if (menuOptionData.menuType == MenuType.Controller) {
            //switch controller type:
        }
    }

    void LoadScene(string levelName) {
        SceneManager.LoadScene(levelName);
    }
}

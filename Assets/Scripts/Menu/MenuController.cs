using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MenuTypes;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private int currentOption;
    private GameObject[] options;
    //give a parent that holds all initial menu options
    public GameObject parent;
    private int lastAxis = 0;

    void Start() {
        //find the menu options in the parent
        LoadContainer(parent);
    }

    void Update() {
        if (Input.GetAxis("Vertical") < 0 && lastAxis > -1) {
            MoveCursorDown();
        } else if (Input.GetAxis("Vertical") > 0 && lastAxis < 1) {
            MoveCursorUp();
        //keyboard support
        } else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
            MoveCursorUp();
        } else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
            MoveCursorDown();
        }

        if (Input.GetAxis("Vertical") > -0.01 && Input.GetAxis("Vertical") < 0.01) {
            lastAxis = 0;
        }

        //joystick button 1 = x (down) for ps4 controller, b (right) for xbox360 thanks devs
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.JoystickButton1)) {
            if (currentOption == null) {
                currentOption = 0;
            } else {
                ClickMenuOption();
            }
        }
        //button 2 is right (circle) on ps4 and left (x) on xbox360
        else if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton2)) {
            //already on top
            if (parent.name != "MenuOptionsParent") {
                parent = parent.transform.parent.gameObject;
                ToggleOptions(options, false);
                LoadContainer(parent);
            }
        }
    }

    void MoveCursorDown() {
        //stop moving down if at the bottom or user has not moved stick
        if (currentOption == null && options.Length > 1) {
            currentOption = 1;
        } else if (currentOption < options.Length - 1) {
            UpdateHighlight(false);
            currentOption++;
        }
        UpdateHighlight(true);
        lastAxis = -1;
    }

    void MoveCursorUp() {
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
            options[currentOption].GetComponent<Text>().color = new Color(255,0,0,1);
        } else {
            options[currentOption].GetComponent<Text>().color = new Color(0,0,0,1);
        }
    }

    void dismissHighlight() {
        foreach (var txt in options) {
            txt.GetComponent<Text>().color = new Color(0,0,0,1);
        }
    }

    void ClickMenuOption() {
        var menuOptionData = options[currentOption].GetComponent<MenuOptionData>();
        if (menuOptionData.menuType == MenuType.Load) {
            LoadScene(menuOptionData.levelName);
        } else if (menuOptionData.menuType == MenuType.Exit) {
            Application.Quit();
        } else if (menuOptionData.menuType == MenuType.Container) {
            parent = options[currentOption];
            ToggleOptions(options, false);
            LoadContainer(parent);
        } else if (menuOptionData.menuType == MenuType.Controller) {
            //switch controller type:
        }
    }

    void LoadScene(string levelName) {
        SceneManager.LoadScene(levelName);
    }

    void LoadContainer(GameObject obj) {
        options = new GameObject[obj.transform.childCount];
        for (int i = 0; i < parent.transform.childCount; i++) {
            options[i] = parent.transform.GetChild(i).gameObject;
        }
        ToggleOptions(options, true);
        currentOption = 0;
        dismissHighlight();
        UpdateHighlight(true);
    }

    void ToggleOptions(GameObject[] objs, bool show) {
        foreach (var obj in objs) {
            obj.GetComponent<Text>().enabled = show;
        }
    }
}

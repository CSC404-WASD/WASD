using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MenuTypes;
using UnityEngine.SceneManagement;
using System;

public class MenuController : MonoBehaviour
{
    private int currentOption = 0;
    private GameObject[] options;
    //give a parent that holds all initial menu options
    public GameObject parent;
    private int lastAxis = 0;
    ControllerLayouts clayout;
    private float lastPress;
    public float maxFreq = 0.2f;

    void Start() {
        //find the menu options in the parent
        clayout = ControllerLayouts.instance;
        LoadContainer(parent);
        lastPress = Time.time;
    }

    void Update() {
        if(Time.time - lastPress > maxFreq)
        {
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
        }

        if (Input.GetAxis("Vertical") > -0.05 && Input.GetAxis("Vertical") < 0.05) {
            lastAxis = 0;
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(clayout.downButton())) {
            {
                ClickMenuOption();
            }
        }

        else if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(clayout.rightButton())) {
            //already on top
            if (parent.name != "MenuOptionsParent") {
                parent = parent.transform.parent.gameObject;
                ToggleOptions(options, false);
                LoadContainer(parent);
            }
        }
    }

    void MoveCursorDown() {
        lastPress = Time.time;
        if (currentOption < options.Length - 1) {
            UpdateHighlight(false);
            currentOption++;
        }
        UpdateHighlight(true);
        lastAxis = -1;
    }

    void MoveCursorUp() {
        lastPress = Time.time;
        if (currentOption > 0) {
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
            clayout.toggleLayout();
            options[currentOption].GetComponent<Text>().text = String.Format("Current Controller: {0}", clayout.cType) ;
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
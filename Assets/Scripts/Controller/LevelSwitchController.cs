using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelSwitchController : MonoBehaviour
{

    private static LevelSwitchController _instance;
    public static LevelSwitchController instance {get {return _instance;}}
    //not sure how to use scene variables so just use 
    public String[] levelNameList;
    public bool onLevelSequence = false;
    private int _index = 0;

    void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
        DontDestroyOnLoad(transform.gameObject);
    }

    public String GetCurrentLevel() {
        return levelNameList[_index];
    }

    public void NextLevel() {
        this._index += 1;
    }

    public void ResetLevelList() {
        this._index = 0;
    }

    public bool NoMoreLevels() {
        return _index >= levelNameList.Length;
    }
}

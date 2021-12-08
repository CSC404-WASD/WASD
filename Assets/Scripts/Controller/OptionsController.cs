using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsController : MonoBehaviour
{
    
    private static OptionsController _instance;
    public static OptionsController instance {get {return _instance;}}

    private bool triggerDeplete = false;

    void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
        DontDestroyOnLoad(transform.gameObject);
    }
    public bool IsTriggerDeplete() {
        return triggerDeplete;
    }

    public void ToggleTriggerDeplete() {
        triggerDeplete = !triggerDeplete;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerLayouts : MonoBehaviour
{
    private static ControllerLayouts _instance;
    public static ControllerLayouts instance {get {return _instance;}}

    private ControllerLayout layout;
    public ControllerType cType;

    void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
        DontDestroyOnLoad(transform.gameObject);
    }

    void Start() {
        cType = ControllerType.XBOX360;
        layout = new Xbox360Layout();
    }

    public void setLayout(ControllerType ctype) {
        if (ctype == ControllerType.XBOX360) {
            layout = new Xbox360Layout();
        } else if (ctype == ControllerType.PS4) {
            layout = new PS4Layout();
        }
    }

    public void toggleLayout() {
        if (cType == ControllerType.XBOX360) {
            layout = new PS4Layout();
            cType = ControllerType.PS4;
        } else if (cType == ControllerType.PS4) {
            layout = new Xbox360Layout();
            cType = ControllerType.XBOX360;
        }
    }
    public KeyCode upButton() { return layout.upButton(); }
    public KeyCode downButton() { return layout.downButton(); }
    public KeyCode leftButton() { return layout.leftButton(); }
    public KeyCode rightButton() { return layout.rightButton(); }
    public KeyCode pauseButton() { return layout.pauseButton(); }
    public KeyCode restartButton() { return layout.restartButton(); }
}

public class ControllerLayout
{
    protected KeyCode upButtonMap;
    protected KeyCode leftButtonMap;
    protected KeyCode rightButtonMap;
    protected KeyCode downButtonMap;
    protected KeyCode pauseButtonMap;
    protected KeyCode restartButtonMap;

    public KeyCode upButton() { return upButtonMap; }
    public KeyCode downButton() { return downButtonMap; }
    public KeyCode leftButton() { return leftButtonMap; }
    public KeyCode rightButton() { return rightButtonMap; }
    public KeyCode pauseButton() { return pauseButtonMap; }
    public KeyCode restartButton() { return restartButtonMap; }
}

public class Xbox360Layout : ControllerLayout {
    public Xbox360Layout() {
        upButtonMap = KeyCode.JoystickButton3;
        leftButtonMap = KeyCode.JoystickButton2;
        rightButtonMap = KeyCode.JoystickButton1;
        downButtonMap = KeyCode.JoystickButton0;

        pauseButtonMap = KeyCode.JoystickButton6;
        restartButtonMap = KeyCode.JoystickButton7;    
    }

}

public class PS4Layout: ControllerLayout {
    public PS4Layout() {
        upButtonMap = KeyCode.JoystickButton3;
        leftButtonMap = KeyCode.JoystickButton0;
        rightButtonMap = KeyCode.JoystickButton2;
        downButtonMap = KeyCode.JoystickButton1;

        pauseButtonMap = KeyCode.JoystickButton8;
        restartButtonMap = KeyCode.JoystickButton9;
    }

}

public enum ControllerType {
    PS4,
    XBOX360
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MenuTypes;

public class MenuOptionData : MonoBehaviour
{
    public MenuType menuType;
    public string levelName;
}

namespace MenuTypes
{
    public enum MenuType {
        Load,
        Container,
        Exit,
        Controller,
        NextLevel,
        Back,
        Restart,
        ExitPause,
        TriggerDeplete
    }
}

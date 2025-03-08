using UnityEngine;

public enum Menu
{
    Main_Menu,
    Pause_Menu,
}

[CreateAssetMenu(fileName = "NewMenu", menuName = "Scene Data/Menu")]
public class MenuSO : GameSceneSO
{
    [Header("Menu Specific")]
    public Menu menuType;
}

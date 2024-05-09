using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private static MenuManager _instance = null;
    public static MenuManager instance => _instance;

    [SerializeField] private MainMenu _mainMenu;
    public MainMenu mainMenu => _mainMenu;
    [SerializeField] private LevelSelectionMenu _levelSelectionMenu;

    public LevelSelectionMenu levelSelectionMenu => _levelSelectionMenu;
    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            return;
        }
        else if(_instance.gameObject.GetInstanceID() != this.gameObject.GetInstanceID()) {
            Destroy(this.gameObject);
        }
    }

    
}

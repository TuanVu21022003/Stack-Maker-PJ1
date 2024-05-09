using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionMenu : MonoBehaviour
{
    [SerializeField] private LevelSelectionUI levelSelectionUI;
    [SerializeField] private Button backButton;

    private void Start()
    {
        backButton.onClick.AddListener(() =>
        {
            MenuManager.instance.mainMenu.gameObject.SetActive(true);
        });
    }
}

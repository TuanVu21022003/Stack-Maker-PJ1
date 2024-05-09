using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button buttonPlay;

    private void Start()
    {
        buttonPlay.onClick.AddListener(() =>
        {
            OnHandleButtonPlay();
        });
    }

    public void OnHandleButtonPlay()
    {
        MenuManager.instance.mainMenu.gameObject.SetActive(false);
    }
}

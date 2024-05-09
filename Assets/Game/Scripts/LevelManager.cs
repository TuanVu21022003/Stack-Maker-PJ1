using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private static LevelManager _instance = null;
    public static LevelManager instance => _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            return;
        }
        else if (_instance.gameObject.GetInstanceID() != this.gameObject.GetInstanceID())
        {
            Destroy(this.gameObject);
        }
    }

    public void OnHandleLevelButton(string textIndex)
    {
        MenuManager.instance.levelSelectionMenu.gameObject.SetActive(false);
        GameObject mapPrefab = Resources.Load<GameObject>($"{PathConstants.PATH_MAP}Map_{textIndex}");
        Debug.Log($"{PathConstants.PATH_MAP}Map_{textIndex}");
        GameObject map = Instantiate(mapPrefab);
        GameManager.instance.ResumeGame();

    }
}

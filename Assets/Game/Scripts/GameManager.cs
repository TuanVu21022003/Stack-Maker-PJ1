using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;
    public static GameManager instance => _instance;
    private bool isPaused = false;

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

    private void Start()
    {
        //PauseTheGame();
    }

    public void PauseTheGame()
    {
        Time.timeScale = 0f; // Dừng thời gian trong game
        isPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f; // Tiếp tục thời gian trong game
        isPaused = false;
    }
}

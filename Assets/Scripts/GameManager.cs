using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    private bool _isGamePaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isGamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        _isGamePaused = true;
        UIManager.Instance.SetPauseImage(_isGamePaused);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        _isGamePaused = false;
        UIManager.Instance.SetPauseImage(_isGamePaused);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField]
    private float _generatingChaserHeight = 100f;

    private bool _isGamePaused = false;
    private bool _isGeneratingChaser = false;

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

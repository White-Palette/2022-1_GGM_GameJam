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

        //if(RealtimeLeaderboardManager.Instance.GetNameById()
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        _isGamePaused = true;
        MouseManager.Show(true);
        MouseManager.Lock(false);
        UIManager.Instance.SetPauseImage(_isGamePaused);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        _isGamePaused = false;
        MouseManager.Show(false);
        MouseManager.Lock(true);
        UIManager.Instance.SetPauseImage(_isGamePaused);
    }

    public void UpdateDeadLog(int id)
    {
        MultiLogManager.Instance.DeadLog(Random.Range(2, 5), RealtimeLeaderboardManager.Instance.GetNameById(id));
    }

}

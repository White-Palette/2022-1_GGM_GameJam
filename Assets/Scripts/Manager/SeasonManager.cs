using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeasonManager : MonoSingleton<SeasonManager>
{
    private SeasonContainer seasonContainer = null;
    private List<Pillar> _towers = null;
    private Season _currentSeason = Season.None;
    private int _currentSeasonIndex = 0;

    private void Awake()
    {
        seasonContainer = Resources.Load<SeasonContainer>("SeasonContainer");
        _currentSeason = Season.Winter;
    }

    private IEnumerator Start()
    {
        while (true)
        {
            yield return null;
            if ((int)(PlayerController.Instance.Height / 300f) > _currentSeasonIndex)
            {
                ++_currentSeasonIndex;
                ChangeSeason();
            }
        }
    }

    public void ChangeSeason()
    {
        Debug.Log("Change Season");

        if (_currentSeason == Season.Spring)
        {
            _currentSeason = Season.Summer;
        }
        else if (_currentSeason == Season.Summer)
        {
            _currentSeason = Season.Autumn;
        }
        else if (_currentSeason == Season.Autumn)
        {
            _currentSeason = Season.Winter;
        }
        else if (_currentSeason == Season.Winter)
        {
            _currentSeason = Season.Spring;
        }
        else
        {
            _currentSeason = Season.Winter;
        }

        Camera.main.backgroundColor = seasonContainer.GetSeasonEffect(_currentSeason)._backgroundColor;

        foreach (var tower in _towers)
        {
            tower.SetTopColor(seasonContainer.GetSeasonEffect(_currentSeason)._topColor, 0.5f);
            tower.SetBodyColor(seasonContainer.GetSeasonEffect(_currentSeason)._bodyColor, 0.5f);
        }
    }

    public void ChangeSeason(Season season)
    {
        _currentSeason = season;
        Camera.main.backgroundColor = seasonContainer.GetSeasonEffect(_currentSeason)._backgroundColor;

        foreach (var tower in _towers)
        {
            tower.SetTopColor(seasonContainer.GetSeasonEffect(_currentSeason)._topColor, 0.5f);
            tower.SetBodyColor(seasonContainer.GetSeasonEffect(_currentSeason)._bodyColor, 0.5f);
        }
    }

    public void AddTower(Pillar tower)
    {
        if (_towers == null)
        {
            _towers = new List<Pillar>();
        }

        if (_towers.Find(x => x == tower) == null)
        {
            _towers.Add(tower);
        }
        ChangeSeason(_currentSeason);
    }
}

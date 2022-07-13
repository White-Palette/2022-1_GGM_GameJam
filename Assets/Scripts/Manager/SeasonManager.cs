using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeasonManager : MonoSingleton<SeasonManager>
{
    private SeasonContainer seasonContainer = null;
    private GameObject[] _towers = null;

    private void Awake()
    {
        seasonContainer = Resources.Load<SeasonContainer>("SeasonContainer");
    }

    public void ChangeSeason(Season season)
    {

    }
}

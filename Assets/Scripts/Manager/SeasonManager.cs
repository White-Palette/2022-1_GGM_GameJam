using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Season
{
    Spring,
    Summer,
    Autumn,
    Winter
}

public class SeasonManager : MonoSingleton<SeasonManager>
{
    public void ChangeSeason(Season season)
    {

    }
}

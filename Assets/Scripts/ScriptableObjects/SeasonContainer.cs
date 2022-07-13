using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum Season
{
    Spring,
    Summer,
    Autumn,
    Winter
}

[System.Serializable]
public class SeasonEffect
{
    public Season Season;
    public Color _topColor;
    public Color _bodyColor;
    public GameObject Effect;
}

[CreateAssetMenu(fileName = "SeasonContainer", menuName = "")]
public class SeasonContainer : ScriptableObject
{
    public SeasonEffect[] SeasonEffects;

    public SeasonEffect GetSeasonEffect(Season season)
    {
        return SeasonEffects.FirstOrDefault(x => x.Season == season);
    }
}

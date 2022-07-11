using UnityEngine;

[System.Serializable]
public class SoundSource
{
    public Sound Sound;
    public AudioClip AudioClip;
}

[System.Serializable]
public class MusicSource
{
    public Music Music;
    public AudioClip AudioClip;
}

[CreateAssetMenu(fileName = "SoundContainer", menuName = "")]
public class SoundContainer : ScriptableObject
{
    public SoundSource[] SoundSources;
    public MusicSource[] MusicSources;
}
using UnityEngine;
using System.Collections;
using System.Linq;

public class SoundManager : MonoSingleton<SoundManager>
{
    private SoundContainer soundContainer;
    private Music currentMusic;
    private AudioSource currentMusicSource;

    private void Awake()
    {
        soundContainer = Resources.Load<SoundContainer>("SoundContainer");
    }

    public void PlaySound(Sound sound, float volume = 1f)
    {
        if (sound == Sound.None)
            return;
        
        SoundSource soundSource = soundContainer.SoundSources.FirstOrDefault(x => x.Sound == sound);
        if (soundSource == null)
            return;

        PoolManager<AudioObject>.Get(transform).PlayOneShot(soundSource.AudioClip, volume);
    }

    public void PlaySound(Music music)
    {
        if (music == Music.None)
            return;
        
        if (currentMusic == music)
            return;

        currentMusic = music;
        MusicSource soundSource = soundContainer.MusicSources.FirstOrDefault(x => x.Music == music);
    }
}
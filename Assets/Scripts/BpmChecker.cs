using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BpmChecker : MonoSingleton<BpmChecker>
{
    [SerializeField] float offset = 0;
    [SerializeField] float bpm = 120;

    private float time = 0;
    private bool isPlaying = false;
    private int beat = 0;

    private void Update()
    {
        if (isPlaying)
        {
            time += Time.deltaTime;
            if (time >= (60f / bpm) + offset)
            {
                time = 0;
                beat++;
                SoundEffect.Play(Sound.Test);
            }
        }
    }

    public float CheckBeat()
    {
        float a = time;
        float b = (60f / bpm) + offset - time;
        return a < b ? a : b;
    }

    public void StartBpm()
    {
        isPlaying = true;
    }

    public void StopBpm()
    {
        isPlaying = false;
    }
}

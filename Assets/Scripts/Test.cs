using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Awake()
    {
        SoundEffect.Play(Sound.Test);
        GetComponent<BpmChecker>().StartBpm();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log($"BpmChecker.Instance.CheckBeat() = {BpmChecker.Instance.CheckBeat()}");
            SoundEffect.Play(Sound.Test);
        }
    }
}

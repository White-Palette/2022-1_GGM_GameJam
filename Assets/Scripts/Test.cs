using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Awake()
    {
        SoundEffect.Play(Sound.Test);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            SoundEffect.Play(Sound.Test);
        }
    }
}

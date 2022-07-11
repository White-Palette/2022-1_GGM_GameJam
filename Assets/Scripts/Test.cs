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
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * 10f;
        float y = Input.GetAxis("Vertical") * Time.deltaTime * 10f;

        transform.Translate(x, y, 0);
    }
}

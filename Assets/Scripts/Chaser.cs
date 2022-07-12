using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : MonoBehaviour
{
    private Animator _animator = null;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Eat()
    {
        _animator.SetTrigger("Bite");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Eat();
        }
    }
}

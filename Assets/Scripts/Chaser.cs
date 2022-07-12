using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : MonoBehaviour
{
    private Animator _animator = null;
    [SerializeField]
    private float _speed = 1f;

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
        Move();
    }

    public void Move()
    {
        Vector3 playerDir = PlayerController.Instance.transform.position - transform.position;
        playerDir.Normalize();
        transform.position += playerDir * _speed * Time.deltaTime;
    }
}

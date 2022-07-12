using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : MonoBehaviour
{
    private Animator _animator = null;
    [SerializeField]
    private float _speed = 1f;

    private bool _isAddingSpeed = false;

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
        if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) > 200f)
        {
            AddSpeed(0.1f);
        }
        Move();
    }

    public void Move()
    {
        Vector3 playerDir = PlayerController.Instance.transform.position - transform.position;
        playerDir.Normalize();
        transform.position += playerDir * _speed * Time.deltaTime;
    }

    public void AddSpeed(float speed)
    {
        if (!_isAddingSpeed)
        {
            _speed += speed;
            StartCoroutine(AddSpeedCoroutine());
        }
    }

    private IEnumerator AddSpeedCoroutine()
    {
        _isAddingSpeed = true;
        yield return new WaitForSeconds(10f);
        _isAddingSpeed = false;
    }
}

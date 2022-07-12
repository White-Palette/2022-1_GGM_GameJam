using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : MonoBehaviour
{
    private Animator _animator = null;
    [SerializeField]
    private float _speed = 1f;

    private bool _isAddingSpeed = false;

    private float _distance = 0f;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Eat()
    {
        _animator.SetTrigger("Bite");
        PlayerController.Instance.Dead();
    }

    void Update()
    {
        // 여기서 속도 빨라지게 해줌
        _distance = Vector2.Distance(transform.position, PlayerController.Instance.transform.position);
        if (_distance > 100f)
        {
            AddSpeed(5f);
        }

        if (_distance < 30f)
        {
            CameraManager.Instance.Noise(0.5f, 15);
        }

        if (_distance < 5f)
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guardian : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

    }

    public void Attack1()
    {
        animator.SetTrigger("Attack1");
    }
    public void Attack2()
    {
        animator.SetTrigger("Attack2");
    }
    public void Hit()
    {
        animator.SetTrigger("Hit");
    }
}

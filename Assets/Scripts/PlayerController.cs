using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoSingleton<PlayerController>
{
    [SerializeField] Pillar currentPillar = null;
    [SerializeField] AnimationCurve jumpCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    [SerializeField] AnimationCurve speedCurve = AnimationCurve.EaseInOut(1, 1, 0, 0);
    [SerializeField] ParticleSystem landing;

    private ParticleSystem particle;
    private Animator animator;
    private bool isMoving = false;
    private float waitTime = 0;
    private float _height = 0f;

    private void Awake()
    {
        ServerManager.Instance.OnConnected += () => {
            StartCoroutine(Connected());
        };
    }

    private IEnumerator Connected()
    {
        while (true)
        {
            ServerManager.Instance.SendHeight(_height);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        particle = transform.Find("Hit").GetComponent<ParticleSystem>();
        MoveToPillar(currentPillar);
    }

    private void Update()
    {
        if (!isMoving)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (currentPillar.LeftPillar != null)
                {
                    MoveToPillar(currentPillar.LeftPillar);
                    transform.localScale = new Vector3(-0.8f, 0.8f, 1);
                    animator.SetBool("IsJump", true);
                }
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (currentPillar.RightPillar != null)
                {
                    MoveToPillar(currentPillar.RightPillar);
                    transform.localScale = new Vector3(0.8f, 0.8f, 1);
                    animator.SetBool("IsJump", true);
                }
            }

            waitTime += Time.deltaTime;

            if (waitTime > 0.2f)
            {
                ComboManager.Instance.ResetCombo();
            }
        }
    }

    public void PlayerWin()
    {
        animator.SetTrigger("Attack");
    }


    public void MoveToPillar(Pillar pillar)
    {
        isMoving = true;

        if (currentPillar.LeftPillar != null)
        {
            if (currentPillar.LeftPillar != pillar)
            {
                currentPillar.LeftPillar.Disable();
            }
        }
        if (currentPillar.RightPillar != null)
        {
            if (currentPillar.RightPillar != pillar)
            {
                currentPillar.RightPillar.Disable();
            }
        }

        currentPillar.Disable();
        currentPillar = pillar;
        SoundManager.Instance.PlaySound(Effect.Jump);

        currentPillar.TowerEvent();
        currentPillar.Generate();

        if (waitTime < 0.1f)
        {
            ComboManager.Instance.AddCombo();
        }

        transform.DOJump(pillar.transform.position + Vector3.up * 1.7f, 2f, 1, JumpDuration()).SetEase(jumpCurve).OnComplete(() =>
        {
            isMoving = false;
            animator.SetBool("IsJump", false);
            landing.transform.position = gameObject.transform.position;
            landing.Play();
            waitTime = 0;
        });
    }

    private float JumpDuration()
    {
        Debug.Log($"{speedCurve.Evaluate(ComboManager.Instance.Combo / 50f)}");
        return speedCurve.Evaluate(ComboManager.Instance.Combo / 50f);
    }

    public float Height
    {
        get
        {
            _height = transform.position.y;
            if (_height < 0f)
            {
                _height = 0f;
            }
            return _height;
        }
    }

    public void Dead()
    {
        particle.Play();
        UserData.Cache.Height = Height;
        UserData.Cache.MaxCombo = ComboManager.Instance.MaxCombo;
        SoundManager.Instance.PlaySound(Effect.Die);
        Fade.Instance.FadeOutToGameOverScene();
    }
}

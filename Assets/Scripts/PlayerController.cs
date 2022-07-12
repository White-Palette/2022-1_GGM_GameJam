using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoSingleton<PlayerController>
{
    [SerializeField] Pillar currentPillar = null;
    [SerializeField] Color nextPillarColor = Color.white;
    [SerializeField] Color currentPillarColor = Color.white;
    [SerializeField] Color previousPillarColor = Color.white;
    [SerializeField] AnimationCurve jumpCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private Animator animator;
    private bool isMoving = false;
    private float waitTime = 0;
    private float _height = 0f;

    private void Start()
    {
        animator = GetComponent<Animator>();
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
            currentPillar.LeftPillar.SpriteRenderer.DOColor(pillar == currentPillar.LeftPillar ? Color.white : previousPillarColor, 0.2f);
        }
        if (currentPillar.RightPillar != null)
        {
            currentPillar.RightPillar.SpriteRenderer.DOColor(pillar == currentPillar.RightPillar ? Color.white : previousPillarColor, 0.2f);
        }

        currentPillar.SpriteRenderer.DOColor(previousPillarColor, 0.2f);
        currentPillar = pillar;

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
            waitTime = 0;
        });
    }

    private float JumpDuration()
    {
        // 이거 공식 사용해서 할까하는데 밸런스 이상해질까바 안정적으로 이렇게 함 일단은 점프 속도 조절해주는거
        if (ComboManager.Instance.Combo > 25)
        {
            return 0.5f;
        }
        else if (ComboManager.Instance.Combo > 20)
        {
            return 0.6f;
        }
        else if (ComboManager.Instance.Combo > 15)
        {
            return 0.7f;
        }
        else if (ComboManager.Instance.Combo > 10)
        {
            return 0.8f;
        }
        else if (ComboManager.Instance.Combo > 5)
        {
            return 0.9f;
        }
        else
        {
            return 1f;
        }
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
        UserData.Cache.Height = Height;
        UserData.Cache.MaxCombo = ComboManager.Instance.MaxCombo;
        // TODO: Goto GameOver Scene
        Fade.Instance.FadeOutToGameOverScene();
    }
}

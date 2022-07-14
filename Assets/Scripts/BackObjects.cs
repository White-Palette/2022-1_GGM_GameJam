using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackObjects : MonoBehaviour
{
    [SerializeField] float multiplier = 1f;

    private List<SpriteRenderer> _topSpriteRenderer = null;
    private List<SpriteRenderer> _bodySpriteRenderer = null;

    private void Awake()
    {
        _topSpriteRenderer = new List<SpriteRenderer>();
        _bodySpriteRenderer = new List<SpriteRenderer>();

        foreach (Transform child in transform)
        {
            _bodySpriteRenderer.Add(child.GetChild(0).GetComponent<SpriteRenderer>());
            _topSpriteRenderer.Add(child.GetChild(1).GetComponent<SpriteRenderer>());
        }
    }

    private void Update()
    {
        Vector2 pos = Camera.main.transform.position;
        pos *= multiplier;
        pos *= -1;
        transform.localPosition = pos;

        foreach (Transform child in transform)
        {
            Vector2 childPos = child.position - Camera.main.transform.position;

            float value = 10f;

            if (childPos.x < -value)
            {
                child.localPosition += new Vector3(value * 2, 0, 0);
            }

            if (childPos.x > value)
            {
                child.localPosition -= new Vector3(value * 2, 0, 0);
            }

            if (childPos.y < -value)
            {
                child.localPosition += new Vector3(0, value * 2, 0);
            }

            if (childPos.y > value)
            {
                child.localPosition -= new Vector3(0, value * 2, 0);
            }
        }
    }

    private void ChangeColor()
    {

    }
}

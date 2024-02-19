using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlincoMultiplier : MonoBehaviour
{
    [SerializeField] private float _multiplier;
    public Action<float> BallDroped;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Ball ball))
        {
            BallDroped?.Invoke(_multiplier * ball.GetCurrentMoney());
        }
    }
}

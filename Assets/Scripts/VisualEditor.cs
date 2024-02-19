using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualEditor : MonoBehaviour
{
    [SerializeField] private List<VisualBall> _visualBalls;
    [SerializeField] private List<VisualBackground> _visualBackgrounds;
    [SerializeField] private BackgroundChanger _backgroundChanger;

    private void OnEnable()
    {
        foreach (var item in _visualBalls)
        {
            item.ItemSelected += UpdateBalls;
        }
        foreach (var item in _visualBackgrounds)
        {
            item.ItemSelected += UpdateBack;
        }
        UpdateBalls();
        UpdateBack();
    }

    private void OnDisable()
    {
        foreach (var item in _visualBalls)
        {
            item.ItemSelected -= UpdateBalls;
        }
        foreach (var item in _visualBackgrounds)
        {
            item.ItemSelected -= UpdateBack;
        }
    }

    private void UpdateBalls()
    {
        var balls = SaveSystem.LoadData<BallsSaveData>();
        for (int i = 1; i < balls.UnlockedBalls.Count; i++)
        {
            if (balls.UnlockedBalls[i])
            {
                _visualBalls[i - 1].UnlockItem();
            }
            else
            {
                _visualBalls[i - 1].LockItem();
            }
        }
        foreach (var item in _visualBalls)
        {
            item.UnselectItem();
        }
        if(balls.CurrentBall != 0)
        {
            _visualBalls[balls.CurrentBall - 1].SelectItem();
        }
    }

    private void UpdateBack()
    {
        var back = SaveSystem.LoadData<BackgroundsSaveData>();
        for (int i = 1; i < back.UnlockedBackgrounds.Count; i++)
        {
            if (back.UnlockedBackgrounds[i])
            {
                _visualBackgrounds[i - 1].UnlockItem();
            }
            else
            {
                _visualBackgrounds[i - 1].LockItem();
            }
        }
        foreach (var item in _visualBackgrounds)
        {
            item.UnselectItem();
        }
        if (back.CurrentBackground != 0)
        {
            _visualBackgrounds[back.CurrentBackground - 1].SelectItem();
        }
        _backgroundChanger.UpdateBackground();
    }
}

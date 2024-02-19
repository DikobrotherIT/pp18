using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlincoGamefield : MonoBehaviour
{
    [SerializeField] private Ball _ball;
    [SerializeField] private List<PlincoMultiplier> _plincoMultipliers;
    private GameController _gameController;

    public void Init(GameController gameController)
    {
        foreach (var item in _plincoMultipliers)
        {
            item.BallDroped += OnBallDroped;
        }
        _gameController = gameController;
    }

    public void DropBall(int money)
    {
        _ball.DropBall(money);
    }

    private void OnBallDroped(float win)
    {
        StartCoroutine(ResetGameField(win));
    }

    private IEnumerator ResetGameField(float win)
    {
        _gameController.PlayDropSFX();
        yield return new WaitForSeconds(2f);
        _ball.ResetBallPosition();
        int currentWin = (int)win;
        _gameController.OnRoundEnded(currentWin);
    }
}

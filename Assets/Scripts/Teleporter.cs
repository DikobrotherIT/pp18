using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Transform _teleportPoint;
    [SerializeField] private Teleporter _secondTeleporter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Ball ball))
        {
            ball.FreezeBall();
            ball.transform.position = _secondTeleporter.GetTeleportPoint();
            ball.DropBall(ball.GetCurrentMoney());
        }
    }

    private Vector2 GetTeleportPoint()
    {
        //int random = Random.Range(0, 2);
        //float horizontallOffset = 0f;
        //if(random == 0)
        //{
        //    horizontallOffset = Random.Range(-0.8f, -0.1f);
        //}
        //else
        //{
        //    horizontallOffset = Random.Range(0.1f, 0.8f);
        //}
        return _teleportPoint.position/* + new Vector3(horizontallOffset, 0, 0)*/;
    }
}

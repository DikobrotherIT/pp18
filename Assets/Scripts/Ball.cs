using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private List<Sprite> _sprites;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rb;
    private int _currentMoney;

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.TryGetComponent(out PlincoSphere plancoSphere))
    //    {
    //        Vector2 normal = collision.contacts[0].normal;
    //        _rb.AddForce(-normal * 2f, ForceMode2D.Impulse);
    //    }
    //}

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    private void OnEnable()
    {
        var balls = SaveSystem.LoadData<BallsSaveData>();
        _spriteRenderer.sprite = _sprites[balls.CurrentBall];
    }

    public void ResetBallPosition()
    {
        _rb.constraints = RigidbodyConstraints2D.FreezeAll;
        transform.localPosition = Vector2.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
    }

    public void MultiplyMoney(int multiplier)
    {
        _currentMoney *= multiplier;
        Debug.Log(_currentMoney);
    }

    public void FreezeBall()
    {
        _rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public void UnfreezeBall()
    {
        _rb.constraints = RigidbodyConstraints2D.None;
    }

    public int GetCurrentMoney()
    {
        return _currentMoney;
    }

    public void DropBall(int money)
    {
        _currentMoney = money;
        _rb.constraints = RigidbodyConstraints2D.None;
        int random = Random.Range(0, 2);
        float horizontal = 0;
        if (random == 0)
        {
            horizontal = Random.Range(-0.8f, -0.1f);
        }
        else
        {
            horizontal = Random.Range(0.1f, 0.8f);
        }

        Vector2 randomForce = new Vector2(horizontal, Random.Range(-1f, 0f));  
        _rb.AddForce(randomForce, ForceMode2D.Impulse);
        //_rb.mass = Random.Range(1, 3);
    }
}

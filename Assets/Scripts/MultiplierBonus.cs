using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplierBonus : MonoBehaviour
{
    [SerializeField] private int _multiplier;
    [SerializeField] private List<Sprite> _bonusSprites;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        switch (_multiplier)
        {
            case 2:
                {
                    _spriteRenderer.sprite = _bonusSprites[0];
                    break;
                }
            case 3:
                {
                    _spriteRenderer.sprite = _bonusSprites[1];
                    break;
                }
            case 5:
                {
                    _spriteRenderer.sprite = _bonusSprites[2];
                    break;
                }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Ball ball))
        {
            ball.MultiplyMoney(_multiplier);
            Destroy(gameObject);
        }
    }
}

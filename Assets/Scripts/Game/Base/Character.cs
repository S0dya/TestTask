using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int maxHp = 10;

    [SerializeField] private protected float walkingSpeed = 4;
    [SerializeField] private protected float runSpeed = 7;

    [Header("Other")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;

    private protected int _curHp;

    private protected bool _canMove = true;
    private protected bool _canAttack = true;

    private float _curMovementSpeed;
    private float _curMovementDirection;
    private bool _isFacingLeft;

    protected virtual void Start()
    {
        _curHp = maxHp;

        _curMovementSpeed = walkingSpeed;
    }

    protected virtual void Update()
    {
        rb.velocity = new Vector2(_curMovementDirection * _curMovementSpeed, 0);
    }

    public void SetMovementDirection(float value)
    {
        _curMovementDirection = value;

        if ((_curMovementDirection > 0 && _isFacingLeft)
         || (_curMovementDirection < 0 && !_isFacingLeft))
        {
            Flip();
        }
    }
    public void SetMovementSpeed(float value)
    {
        _curMovementSpeed = value;
    }

    public virtual void ChangeHP(int amount)
    {
        _curHp = Math.Min(maxHp, Math.Max(0, _curHp + amount));
    }

    public virtual void StopAttack()
    {
        _canMove = true;
    }

    private void Flip()
    {
        _isFacingLeft = !_isFacingLeft;
        var localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
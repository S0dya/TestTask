using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private protected int maxHp = 10;

    [SerializeField] private protected float walkSpeed = 4;
    [SerializeField] private protected float runSpeed = 7;

    [Header("Other")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private protected Animator animator;

    private protected int _curHp;

    private protected bool _canMove = true;
    private protected bool _canAttack = true;

    private float _curMovementSpeed;
    private float _curMovementDirection;
    private bool _isFacingLeft;

    private protected int _animatorIDMovementDirection;
    private protected int _animatorIDPunch;
    private protected int _animatorIDKick;

    protected virtual void Start()
    {
        _curHp = maxHp;

        _curMovementSpeed = walkSpeed;

        _animatorIDMovementDirection = Animator.StringToHash("MovementDirection");
        _animatorIDPunch = Animator.StringToHash("Punch");
        _animatorIDKick = Animator.StringToHash("Kick");

    }

    protected virtual void Update()
    {
        rb.velocity = new Vector2(_curMovementDirection * _curMovementSpeed, 0);
    }

    public void SetMovementDirection(float value)
    {
        _curMovementDirection = value;

        animator.SetFloat(_animatorIDMovementDirection, _curMovementDirection);
    }
    public void HandleFacingDirection() => HandleFacingDirection(_curMovementDirection);
    public void HandleFacingDirection(float direction)
    {
        if ((direction > 0 && _isFacingLeft) || (direction < 0 && !_isFacingLeft)) 
            Flip();
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
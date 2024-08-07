using UnityEngine;
using Zenject;

public class Player : Character
{
    [Header("Other")]
    [SerializeField] private PlayerCombat playerCombat;

    //input
    public void Move(float direction)
    {
        if (!_canMove) return;
     
        SetMovementDirection(direction);
        HandleFacingDirection();
    }
    public void StopMove()
    {
        SetMovementDirection(0);
    }
    public void Hit()
    {
        if (!_canAttack) return;

        animator.Play(_animatorIDPunch);

        playerCombat.Hit();

        _canMove = false;
        StopMove();
    }
    public void Kick()
    {
        if (!_canAttack) return;
        
        animator.Play(_animatorIDKick);

        playerCombat.Kick();

        _canMove = false;
        StopMove();
    }


    public override void ChangeHP(int amount)
    {
        base.ChangeHP(amount);

        if (_curHp == 0)
        {
            Debug.Log("Gameover");
        }
    }
}

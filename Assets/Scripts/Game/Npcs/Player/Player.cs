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
    }
    public void StopMove()
    {
        SetMovementDirection(0);


    }
    public void Hit()
    {
        if (!_canAttack) return;
        
        playerCombat.Hit();

        //_canMove = false;
    }
    public void Kick()
    {
        if (!_canAttack) return;
        
        playerCombat.Kick();

        //_canMove = false;
    }


    public override void ChangeHP(int amount)
    {
        base.ChangeHP(amount);

        if (_curHp == 0)
        {
            //die
        }
    }
}

using System;
using UnityEngine;

public class AIStateAttack : AIState
{

    public override void EnterState(Enemy enemy)
    {

    }
    public override void UpdateState(Enemy enemy)
    {
        var targetPosition = enemy.GetAttackTargetPosition();
        var distanceWithTarget = Math.Abs(targetPosition.x - enemy.transform.position.x);

        if (distanceWithTarget > 3)
        {
            enemy.WalkTo(targetPosition);
        }
        else if (distanceWithTarget < 2.5f && enemy.CanAttack())
        {
            enemy.StartAttack();
        }

        enemy.HandleFacingDirection(targetPosition);
    }
    public override void ExitState(Enemy enemy)
    {

    }
}

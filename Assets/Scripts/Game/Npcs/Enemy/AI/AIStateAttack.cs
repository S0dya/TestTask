using UnityEngine;
using System;

using Random = UnityEngine.Random;

public class AIStateAttack : AIState
{
   // private Vector2 _curRoamingPosition;

    public override void EnterState(Enemy enemy)
    {

    }
    public override void UpdateState(Enemy enemy)
    {
        var targetPosition = enemy.GetAttackTargetPosition();
        var distanceWithTarget = Math.Abs(targetPosition.x - enemy.transform.position.x);

        if (distanceWithTarget > 2)
        {
            enemy.WalkTo(targetPosition);
        }
        else
        {
            enemy.SetMovementDirection(0);
           // enemy.WalkTo(GetRandomPosition(0.5f) + targetPosition);
        }

        if (distanceWithTarget < 0.5f && enemy.CanAttack())
        {
            enemy.StartAttack();
        }
    }
    public override void ExitState(Enemy enemy)
    {

    }

    private Vector2 GetRandomPosition(float val)
    {
        return new Vector2(Random.Range(-val, val), Random.Range(-val, val));
    }
}

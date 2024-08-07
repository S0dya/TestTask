using System;
using UnityEngine;

public class AIStatePatrol : AIState
{
    private Vector2 _curTargetPos;

    public override void EnterState(Enemy enemy)
    {
        _curTargetPos = enemy.GetNewPatrolPoint();
    }
    public override void UpdateState(Enemy enemy)
    {
        enemy.WalkTo(_curTargetPos);

        if (Math.Abs(_curTargetPos.x - enemy.transform.position.x) < 0.1f)
        {
            _curTargetPos = enemy.GetNewPatrolPoint();
        }
    }
    public override void ExitState(Enemy enemy)
    {

    }
}

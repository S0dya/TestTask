using System;

public class AIStateRun : AIState
{
    public override void EnterState(Enemy enemy)
    {

    }
    public override void UpdateState(Enemy enemy)
    {
        var targetPosition = enemy.GetAttackTargetPosition();

        enemy.RunTo(targetPosition);
        enemy.HandleFacingDirection();

        if (Math.Abs(targetPosition.x - enemy.transform.position.x) < 6)
        {
            enemy.TransitionToState(enemy.AttackState);
        }
    }
    public override void ExitState(Enemy enemy)
    {

    }
}

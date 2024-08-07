using UnityEngine;

public class AIStateRoam : AIState
{
    private Vector2 _targetPosition;
    private bool _isWaiting;
    private float _waitTime;
    private float _waitTimer;

    public override void EnterState(Enemy enemy)
    {
        _isWaiting = false;

        SetRandomTargetPosition(enemy.transform.position);
    }

    public override void UpdateState(Enemy enemy)
    {
        if (_isWaiting)
        {
            _waitTimer -= Time.deltaTime;
            if (_waitTimer <= 0)
            {
                _isWaiting = false;

                SetRandomTargetPosition(enemy.transform.position);
            }
        }
        else
        {
            enemy.WalkTo(_targetPosition);

            if (Mathf.Abs(enemy.transform.position.x - _targetPosition.x) < 0.1f)
            {
                _isWaiting = true;
                _waitTimer = _waitTime;
            }
        }

        enemy.HandleFacingDirection(enemy.GetAttackTargetPosition());
    }

    public override void ExitState(Enemy enemy)
    {

    }

    private void SetRandomTargetPosition(Vector2 position)
    {
        _targetPosition = position + GetRandomPosition(4f);

        _waitTime = Random.Range(0.1f, 1.5f); 
    }

    private Vector2 GetRandomPosition(float radius)
    {
        return new Vector2(Random.Range(-radius, radius), Random.Range(-radius, radius));
    }
}
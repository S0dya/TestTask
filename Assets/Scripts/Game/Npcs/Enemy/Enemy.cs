using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class Enemy : Character //NPC/Character
{
    [SerializeField] private EnemyCombat enemyCombat;

    [SerializeField] private List<Vector2> patrolPoints;
    

    public readonly AIStatePatrol PatrolState = new();
    public readonly AIStateRun RunState = new();
    public readonly AIStateAttack AttackState = new();
    public readonly AIStateRoam RoamState = new();

    //local
    private EnemyManager _enemyManager;

    private AIState _currentState;

    private Vector2 _curPatrolPoint;
    private Transform _curAttackTargetTransform;
    
    public void Init(EnemyManager enemyManager)
    {
        _enemyManager = enemyManager;
        
        Start();
        
        TransitionToState(PatrolState);

        if (patrolPoints.Count == 0)
        {
            patrolPoints.Add(new Vector2 (Random.Range(transform.position.x - 30, transform.position.x - 20), transform.position.y));
            patrolPoints.Add(new Vector2 (Random.Range(transform.position.x + 30, transform.position.x + 20), transform.position.y));
        }
    }

    protected override void Update()
    {
        _currentState?.UpdateState(this);
    }

    public void TransitionToState(AIState newState)
    {
        _currentState?.ExitState(this);
        _currentState = newState;
        _currentState.EnterState(this);
    }

    public void WalkTo(Vector2 targetPos) => MoveTo(targetPos, walkSpeed);
    public void RunTo(Vector2 targetPos) => MoveTo(targetPos, runSpeed);

    public void SetAttackTarget(Transform transform)
    {
        _curAttackTargetTransform = transform;

        TransitionToState(RunState);
    }

    public void StartAttack()
    {
        _canAttack = _canMove = false;
        SetMovementDirection(0);

        animator.Play(enemyCombat.SetRandomAttack());

        enemyCombat.PerformAttack();

        TransitionToState(RoamState);
    }

    public override void ChangeHP(int amount)
    {
        base.ChangeHP(amount);

        if (amount < 0) _enemyManager.EnemyAttacked();

        if (_curHp == 0) _enemyManager.EnemyDied(this);
    }

    public void HandleFacingDirection(Vector2 targetPos)
    {
        HandleFacingDirection((targetPos - (Vector2)transform.position).normalized.x);
    }

    public override void StopAttack()
    {
        base.StopAttack();

        StartCoroutine(AttackCooldownCoroutine(enemyCombat.GetCooldown()));
    }

    public Vector2 GetNewPatrolPoint()
    {
        if (_curPatrolPoint != null) patrolPoints.Add(_curPatrolPoint);

        _curPatrolPoint = patrolPoints[Random.Range(0, patrolPoints.Count)];
        patrolPoints.Remove(_curPatrolPoint);

        return _curPatrolPoint;
    }
    public Vector2 GetAttackTargetPosition()
    {
        return _curAttackTargetTransform.position;
    }
    
    public bool CanAttack()
    {
        return _canAttack;
    }

    private void MoveTo(Vector2 targetPos, float speed)
    {
        if (!_canMove) return;

        SetMovementDirection((targetPos - (Vector2)transform.position).normalized.x);

        base.Update();
    }

    IEnumerator AttackCooldownCoroutine(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);

        _canAttack = true;

        TransitionToState(AttackState);
    }
}

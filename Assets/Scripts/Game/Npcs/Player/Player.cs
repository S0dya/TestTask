using UnityEngine;
using Zenject;

public class Player : Character
{
    [Header("Other")]
    [SerializeField] private PlayerCombat playerCombat;
    [SerializeField] private PlayerInteraction playerInteraction;

    private UIMain _uiMain;

    [Inject]
    public void Construct(UIMain uiMain)
    {
        _uiMain = uiMain;
    }

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
    public void Run()
    {
        SetMovementSpeed(runSpeed);
    }
    public void StopRun()
    {
        SetMovementSpeed(walkSpeed);
    }
    public void Hit()
    {
        if (!_canAttack) return;

        animator.Play(_animatorIDPunch);
        StartAttack();

        playerCombat.Hit();
    }
    public void Kick()
    {
        if (!_canAttack) return;
        
        animator.Play(_animatorIDKick);
        StartAttack();

        playerCombat.Kick();
    }

    public void Interact()
    {
        switch (playerInteraction.GetInteraction())
        {
            case InteractionDialogue dialogueInteraction: _uiMain.StartDialogue(); break;
            default : break;
        }
    }


    public override void ChangeHP(int amount)
    {
        base.ChangeHP(amount);

        if (_curHp == 0)
        {
            Debug.Log("Gameover");
        }
    }

    private void StartAttack()
    {
        _canMove = false;
        StopMove();
    }
}

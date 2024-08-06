using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerCombat playerCombat;

    [SerializeField] private Animator animator;

    private bool _canMove = true;
    private bool _canHit = true;

    //input
    public void Move(float direction)
    {
        if (!_canMove) return;

        playerMovement.SetMoveement(direction);
    }
    public void StopMove()
    {
        playerMovement.SetMoveement(0);


    }
    public void Hit()
    {
        if (!_canHit) return;
        
        playerCombat.Hit();

        //_canMove = false;
    }
    public void Kick()
    {
        if (!_canHit) return;
        
        playerCombat.Kick();

        //_canMove = false;
    }

    public void StoppedHitting()
    {
        _canMove = true;
    }
}

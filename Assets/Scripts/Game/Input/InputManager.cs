using UnityEngine;
using Zenject;

public class InputManager : MonoBehaviour
{

    
    private Player _player;
    
    private Inputs _input;



    [Inject]
    public void Construct(Player player)
    {
        _player = player;
    }

    private void OnEnable()
    {
        _input = new Inputs();

        _input.InGame.Move.performed += ctx => _player.Move(ctx.ReadValue<float>());
        _input.InGame.Move.canceled += ctx => _player.StopMove();

        _input.InGame.Hit.performed += ctx => _player.Hit();
        _input.InGame.Hit.performed += ctx => _player.Kick();


        _input.Enable();
    }
    private void OnDisable()
    {
        _input.InGame.Move.performed -= ctx => _player.Move(ctx.ReadValue<float>());
        _input.InGame.Move.canceled -= ctx => _player.StopMove();

        _input.InGame.Hit.performed -= ctx => _player.Hit();
        _input.InGame.Hit.performed -= ctx => _player.Kick();

        _input.Disable();
    }
}

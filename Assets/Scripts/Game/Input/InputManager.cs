using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class InputManager : SubjectMonoBehaviour
{

    
    private Player _player;
    private UIMain _uiMain;

    private Inputs _input;

    InputActionMap _inGameInput;
    InputActionMap _inDialogueInput;

    List<InputActionMap> _inputActionMaps = new();

    [Inject]
    public void Construct(Player player, UIMain uiMain)
    {
        _player = player;
        _uiMain = uiMain;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        _input = new Inputs();

        _inGameInput = _input.InGame;
        _inDialogueInput = _input.InDialogue;

        //in game
        _input.InGame.Move.performed += ctx => _player.Move(ctx.ReadValue<float>());
        _input.InGame.Move.canceled += ctx => _player.StopMove();

        _input.InGame.Run.performed += ctx => _player.Run();
        _input.InGame.Run.canceled += ctx => _player.StopRun();

        _input.InGame.Hit.performed += ctx => _player.Hit();
        _input.InGame.Kick.performed += ctx => _player.Kick();

        _input.InGame.Interact.performed += ctx => _player.Interact();

        //in dialogue
        _input.InDialogue.SkipLine.performed += ctx => _uiMain.OnSkipLine();

        _input.Enable();
    }
    protected override void OnDisable()
    {
        base.OnDisable();

        //in game
        _input.InGame.Move.performed -= ctx => _player.Move(ctx.ReadValue<float>());
        _input.InGame.Move.canceled -= ctx => _player.StopMove();

        _input.InGame.Run.performed -= ctx => _player.Run();
        _input.InGame.Run.canceled -= ctx => _player.StopRun();

        _input.InGame.Hit.performed -= ctx => _player.Hit();
        _input.InGame.Kick.performed -= ctx => _player.Kick();

        _input.InGame.Interact.performed -= ctx => _player.Interact();

        //in dialogue
        _input.InDialogue.SkipLine.performed -= ctx => _uiMain.OnSkipLine();

        _input.Disable();
    }

    private void Start()
    {
        _inputActionMaps.Add(_inGameInput);
        _inputActionMaps.Add(_inDialogueInput);

        SwitchActionMaps(_inGameInput);

        Init(new Dictionary<EventEnum, Action>
        {
            { EventEnum.DialogueOpened, OnDialogue},
            { EventEnum.DialogueClosed, OnInGame},
        });
    }

    public void OnInGame() => SwitchActionMaps(_inGameInput);
    public void OnDialogue() => SwitchActionMaps(_inDialogueInput);

    private void SwitchActionMaps(InputActionMap actionMapToEnable)
    {
        DisableActionMaps();

        EnableActionMap(actionMapToEnable);
    }
    private void DisableActionMaps()
    {
        foreach (var actionMap in _inputActionMaps)
        {
            actionMap.Disable();
            DisableActionMap(actionMap);
        }
    }

    void EnableActionMap(InputActionMap actionMap) => actionMap.Enable();
    void DisableActionMap(InputActionMap actionMap) => actionMap.Disable();
}

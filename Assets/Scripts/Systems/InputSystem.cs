using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystem : ComponentSystem
{
    private EntityQuery _inputQuery;

    private InputAction _moveAction;
    private InputAction _sprintAction;
    private InputAction _attackAction;
    private InputAction _reloadAction;

    private float2 _moveInput;
    private float _sprintInput;
    private float _attackInput;
    private float _reloadInput;

    protected override void OnCreate()
    {
        _inputQuery = GetEntityQuery(ComponentType.ReadOnly<UserInputData>(), ComponentType.ReadOnly<InputData>());
    }

    protected override void OnStartRunning()
    {
        _moveAction = new InputAction("Move");
        _moveAction.AddCompositeBinding("Up", "<Keyboard>/w")
            .With("Right", "<Keyboard>/d")
            .With("Left", "<Keyboard>/d")
            .With("Down", "<Keyboard>/s");
        
        _moveAction.performed += context => { _moveInput = context.ReadValue<Vector2>(); };
        _moveAction.started += context => { _moveInput = context.ReadValue<Vector2>(); };
        _moveAction.canceled += context => { _moveInput = context.ReadValue<Vector2>(); };

        _sprintAction = new InputAction("Sprint");
        _sprintAction.AddCompositeBinding("Sprint", "<Keyboard>/leftShift");
        
        _sprintAction.performed += context => { _sprintInput = context.ReadValue<float>(); };
        _sprintAction.started += context => { _sprintInput = context.ReadValue<float>(); };
        _sprintAction.canceled += context => { _sprintInput = context.ReadValue<float>(); };

        _attackAction = new InputAction("Attack");
        _attackAction.AddCompositeBinding("Attack", "<Mouse>/leftButton");
        
        _attackAction.performed += context => { _attackInput = context.ReadValue<float>(); };
        _attackAction.started += context => { _attackInput = context.ReadValue<float>(); };
        _attackAction.canceled += context => { _attackInput = context.ReadValue<float>(); };

        _reloadAction = new InputAction("Reload");
        _reloadAction.AddCompositeBinding("Reload", "<Keyboard>/r");
        
        _reloadAction.performed += context => { _reloadInput = context.ReadValue<float>(); };
        _reloadAction.started += context => { _reloadInput = context.ReadValue<float>(); };
        _reloadAction.canceled += context => { _reloadInput = context.ReadValue<float>(); };
        
        _moveAction.Enable();
        _sprintAction.Enable();
        _attackAction.Enable();
        _reloadAction.Enable();
    }

    protected override void OnStopRunning()
    {
        _moveAction.Disable();
        _sprintAction.Disable();
        _attackAction.Disable();
        _reloadAction.Disable();
    }

    protected override void OnUpdate()
    {
        Entities.With(_inputQuery).ForEach(
            (Entity entity, ref InputData inputData) =>
            {
                inputData.Move = _moveInput;
                inputData.Sprint = _sprintInput;
                inputData.Attack = _attackInput;
                inputData.Reload = _reloadInput;
            });
    }
}

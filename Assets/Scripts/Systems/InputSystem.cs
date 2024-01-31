using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystem : ComponentSystem
{
    private EntityQuery _inputQuery;

    private InputAction _moveAction;
    private InputAction _attackAction;

    private float2 _moveInput;
    private float _attackInput;

    protected override void OnCreate()
    {
        _inputQuery = GetEntityQuery(ComponentType.ReadOnly<UserInputData>(), ComponentType.ReadOnly<InputData>());
    }

    protected override void OnStartRunning()
    {
        _moveAction = new InputAction("move");
        _moveAction.AddCompositeBinding("Up", "<Keyboard>/w")
            .With("Right", "<Keyboard>/d")
            .With("Left", "<Keyboard>/d")
            .With("Down", "<Keyboard>/s");
        
        _moveAction.performed += context => { _moveInput = context.ReadValue<Vector2>(); };
        _moveAction.started += context => { _moveInput = context.ReadValue<Vector2>(); };
        _moveAction.canceled += context => { _moveInput = context.ReadValue<Vector2>(); };

        _attackAction = new InputAction("attack");
        _attackAction.AddCompositeBinding("shoot", "<Mouse>/leftButton");
        
        _attackAction.performed += context => { _attackInput = context.ReadValue<float>(); };
        _attackAction.started += context => { _attackInput = context.ReadValue<float>(); };
        _attackAction.canceled += context => { _attackInput = context.ReadValue<float>(); };
        
        _moveAction.Enable();
        _attackAction.Enable();
    }

    protected override void OnStopRunning()
    {
        _moveAction.Disable();
        _attackAction.Disable();
    }

    protected override void OnUpdate()
    {
        Entities.With(_inputQuery).ForEach(
            (Entity entity, ref InputData inputData) =>
            {
                inputData.Move = _moveInput;
                inputData.Attack = _attackInput;
            });
    }
}

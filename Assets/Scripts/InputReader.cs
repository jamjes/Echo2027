using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    private PlayerInputActions actions;
    public event Action OnJump;
    public event Action OnRewindStart;
    public event Action OnRewindEnd;
    
    public Vector2 Direction { get; private set; }

    private void OnEnable()
    {
        actions.Enable();
        actions.@default.Jump.started += JumpDetected;
        actions.@default.Rewind.started += HandleRewindInput;
        actions.@default.Rewind.canceled += HandleRewindInput;
    }

    private void OnDisable()
    {
        actions.Disable();
        actions.@default.Jump.started -= JumpDetected;
        actions.@default.Rewind.started -= HandleRewindInput;
        actions.@default.Rewind.canceled -= HandleRewindInput;
    }

    private void Awake()
    {
        actions = new PlayerInputActions();
    }

    private void JumpDetected(InputAction.CallbackContext context) => OnJump?.Invoke();

    private void HandleRewindInput(InputAction.CallbackContext context)
    {
        if (context.started) OnRewindStart?.Invoke();
        else if (context.canceled) OnRewindEnd?.Invoke();
    }
    
    private void Update()
    {
        if (actions == null) return;
        
        Direction = actions.@default.Move.ReadValue<Vector2>();
    }
}

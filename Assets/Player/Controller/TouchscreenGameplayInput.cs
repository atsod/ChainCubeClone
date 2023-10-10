using System;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class TouchscreenGameplayInput : MonoBehaviour
{
    public static event Action OnPressed;
    public static event Action OnReleased;
    public static event Action<Vector2> OnReadDelta;

    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        _playerInput.Enable();

        _playerInput.Touch.TouchPress.started += OnTouchPressed;
        _playerInput.Touch.TouchPress.canceled += OnTouchReleased;

        _playerInput.Touch.TouchDelta.performed += OnDeltaPerformed;
    }

    private void OnDisable()
    {
        _playerInput.Disable();
        
        _playerInput.Touch.TouchPress.started -= OnTouchPressed;
        _playerInput.Touch.TouchPress.canceled -= OnTouchReleased;

        _playerInput.Touch.TouchDelta.performed -= OnDeltaPerformed;
    }
    
    private void OnTouchPressed(InputAction.CallbackContext context)
    {
        OnPressed?.Invoke();
    }

    private void OnTouchReleased(InputAction.CallbackContext context)
    {
        OnReleased?.Invoke();
    }

    private void OnDeltaPerformed(InputAction.CallbackContext context)
    {
        OnReadDelta?.Invoke(context.ReadValue<Vector2>());
    }
}

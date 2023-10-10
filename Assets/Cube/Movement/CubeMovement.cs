using System;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    public static event Action OnCubeReleased;

    public Rigidbody CubeRigidbody { private set; get; }

    private Transform _transform;

    private float _deltaSpeed;

    private Vector3 _movementDirection;
    private float _impulseSpeed;

    private bool _isTouchPressed;
    private bool _isPlayerMovingCube;

    private void Awake()
    {
        CubeRigidbody = GetComponent<Rigidbody>();
        CubeRigidbody.useGravity = false;

        _transform = GetComponent<Transform>();

        _deltaSpeed = 0.3f;

        _movementDirection = Vector3.forward;
        _impulseSpeed = 15f;
    }

    private void OnEnable()
    {
        TouchscreenGameplayInput.OnReadDelta += OnDeltaPerformed;

        TouchscreenGameplayInput.OnPressed += OnTouchPressed;
        TouchscreenGameplayInput.OnReleased += OnTouchReleased;
    }

    private void OnDisable()
    {
        TouchscreenGameplayInput.OnReadDelta -= OnDeltaPerformed;

        TouchscreenGameplayInput.OnPressed -= OnTouchPressed;
        TouchscreenGameplayInput.OnReleased -= OnTouchReleased;
    }

    private void OnDeltaPerformed(Vector2 delta)
    {
        if (_isTouchPressed)
        {
            _isPlayerMovingCube = true;

            if (!(float.IsNaN(delta.x) || float.IsNaN(delta.y) 
                || float.IsInfinity(delta.x) || float.IsInfinity(delta.y)))
            {
                _transform.position += _deltaSpeed * Time.deltaTime * (Vector3)delta;
            }
        }
        else if (!_isTouchPressed && _isPlayerMovingCube)
        {
            AddImpulseToCube();

            OnCubeReleased?.Invoke();
            
            DisableComponent();
        }
    }

    private void AddImpulseToCube()
    {
        CubeRigidbody.AddForce(_impulseSpeed * _movementDirection, ForceMode.Impulse);
    }

    private void DisableComponent()
    {
        GetComponent<CubeMovement>().enabled = false;
    }

    private void OnTouchPressed()
    {
        if (!_isTouchPressed)
        {
            CubeRigidbody.useGravity = false;
            _isTouchPressed = true;
        }
    }

    private void OnTouchReleased()
    {
        if (_isTouchPressed)
        {
            CubeRigidbody.useGravity = true;
            _isTouchPressed = false;
        }
    }
}

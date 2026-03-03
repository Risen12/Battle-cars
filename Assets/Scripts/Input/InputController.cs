using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    private PlayerControls _playerControls;
    private Vector2 _movementDirection;

    public event Action AttackPerformed;
    
    public Vector2 MovementDirection => _movementDirection;
    
    private void Awake()
    {
        _playerControls  = new PlayerControls();
    }

    private void OnEnable()
    {
        _playerControls.Enable();
        _playerControls.Player.Attack.started += OnAttack;
    }

    private void OnDisable()
    {
        _playerControls.Disable(); 
        _playerControls.Player.Attack.started -= OnAttack;
    }

    private void Update()
    {
        _movementDirection = _playerControls.Player.Move.ReadValue<Vector2>();
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        AttackPerformed?.Invoke();
    }
}
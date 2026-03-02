using System;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private PlayerControls _playerControls;
    private Vector2 _movementDirection;

    public event Action AttackPerformed;
    
    public Vector2 MovementDirection => _movementDirection;
    
    private void Awake()
    {
        _playerControls  = new PlayerControls();
        _playerControls.Enable();
    }

    private void Update()
    {
        _movementDirection = _playerControls.Player.Move.ReadValue<Vector2>();

        if (_playerControls.Player.Attack.IsPressed())
        {
            AttackPerformed?.Invoke();
        }
    }
}

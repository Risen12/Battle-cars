using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _MoveTime;
    [SerializeField] private Vector3 _velocity;
    
    private void LateUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, _player.position + _offset, ref _velocity, _MoveTime);
        //transform.position = Vector3.Slerp(transform.position, _player.position +  _offset, _MoveTime / Time.deltaTime);
    }
}
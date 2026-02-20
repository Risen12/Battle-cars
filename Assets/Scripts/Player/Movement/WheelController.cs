using System;
using UnityEngine;

namespace Player.Movement
{
    [RequireComponent(typeof(WheelCollider))]
    public class WheelController : MonoBehaviour
    {
        [SerializeField] private Transform _wheelModel;
        [SerializeField] private bool _steerable;
        [SerializeField] private bool _motorized;
        
        private WheelCollider _collider;
        private Vector3 _position;
        private Quaternion _rotation;
        
        public bool Steerable => _steerable;
        public bool Motorized => _motorized;

        private void Awake()
        {
            _collider = GetComponent<WheelCollider>();
        }
        
        private void Update()
        {
            // Get the Wheel collider's world pose values and
            // use them to set the wheel model's position and rotation
            _collider.GetWorldPose(out _position, out _rotation);
            _wheelModel.transform.position = _position;
            _wheelModel.transform.rotation = _rotation;
        }
        
        public WheelCollider GetCollider() => _collider;
    }
}


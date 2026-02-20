using System;
using System.Collections.Generic;
using UnityEngine;


namespace Player.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public class CarMover : MonoBehaviour
    {
        [SerializeField] private InputController _inputController;
        [SerializeField] private float _motorTorque;
        [SerializeField] private float _brakeTorque;
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _steeringRange;
        [SerializeField] private float _steeringRangeAtMaxSpeed;
        [SerializeField] private float _centerOfGravityOffset;

        private WheelController[] _wheels;
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _wheels = GetComponentsInChildren<WheelController>();

            Vector3 centerOfMass = _rigidbody.centerOfMass;
            centerOfMass.y += _centerOfGravityOffset;
            _rigidbody.centerOfMass = centerOfMass;
        }

        private void FixedUpdate()
        {
            Vector2 direction = _inputController.MovementDirection;
            float forwardDirection = direction.y;
            float sideDirection = direction.x;

            float forwardSpeed = Vector3.Dot(transform.forward, _rigidbody.linearVelocity);
            float speedFactor = Mathf.InverseLerp(0, _maxSpeed, Mathf.Abs(forwardSpeed));

            float currentMotorTorque = Mathf.Lerp(_motorTorque, 0, speedFactor);
            float currentSteerRange = Mathf.Lerp(_steeringRange, _steeringRangeAtMaxSpeed, speedFactor);

            bool isAccelerating = Mathf.Sign(forwardDirection) == Mathf.Sign(forwardSpeed);

            foreach (var wheel in _wheels)
            {
                if (wheel.Steerable)
                {
                    wheel.GetCollider().steerAngle = sideDirection * currentSteerRange;
                }

                if (isAccelerating)
                {
                    if (wheel.Motorized)
                    {
                        wheel.GetCollider().motorTorque = forwardDirection * currentMotorTorque;
                    }
                    
                    wheel.GetCollider().brakeTorque = 0f;
                }
                else
                {
                    wheel.GetCollider().motorTorque = 0f;
                    wheel.GetCollider().brakeTorque = Mathf.Abs(forwardDirection) * _brakeTorque;
                }
            }
        }
    }
}


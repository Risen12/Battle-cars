using Player.Movement;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Rigidbody))]
    public class BotMover : MonoBehaviour
    {
        [SerializeField] private float _motorTorque;
        [SerializeField] private float _brakeTorque;
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _steeringRange;
        [SerializeField] private float _steeringRangeAtMaxSpeed;
        [SerializeField] private float _centerOfGravityOffset;
        [SerializeField] private Transform _target;
        [SerializeField] private float _offset;

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
            Vector3 direction = new Vector3(_target.position.x + _offset, 0, _target.position.z + _offset) - transform.position;
            float forwardDirection = direction.x;
            float sideDirection = direction.z;

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
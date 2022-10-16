using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class TransformExtensions
{
    public static Vector3 LocalPositionOffset(this Transform transform, Vector3 positionOffset)
    {
        return transform.position + transform.right * positionOffset.x + transform.up * positionOffset.y + transform.forward * positionOffset.z;
    }
    public static Vector3 LocalRotationOffset(this Transform transform, Vector3 rotationOffset)
    {
        return transform.rotation.eulerAngles + rotationOffset;
    }
}

namespace TransformUtility
{
    public class TransformController
    {
        private Transform _transform;
        // Movement
        private Vector3 _movementVector, _currentMovementSpeed;
        private Vector2 _movementInput, _currentMovement;
        private Vector2 _movementAcc = new Vector2(10.0f, 10.0f), _movementDamp = new Vector2(5.0f, 5.0f);
        private Vector2 _movementClampForward = new Vector2(-2.25f, 2.25f), _movementClampRight = new Vector2(-2.25f, 2.25f);
        public Vector2 movementInput { get { return _movementInput; } set { _movementInput = value; } }
        // Gravity
        private bool _useGravity = true; 
        private Vector3 _gravity, _currentGravity;
        public bool useGravity { get { return _useGravity; } set { _useGravity = value; } }
        public Vector3 gravity { get { return _gravity; } set { _gravity = value; } }
        // Grounded
        private bool _isGrounded = false;
        private Vector3 _groundCastOffset = new Vector3(0.0f, 0.5f, 0.0f);
        private float _groundCastDistance = 0.5f;
        private RaycastHit _groundHit;
        public Vector3 groundCastOffset { get { return _groundCastOffset; } set { _groundCastOffset = value; } }
        // Velocity
        private Vector3 _velocity;
        private Vector3 _prevPosition;
        public Vector3 velocity { get { return _velocity; } }

        public TransformController(Transform transform)
        {
            this._transform = transform;
            this._prevPosition = transform.position;
            this._gravity = Physics.gravity; // Set default Gravity Vector to Physics Gravity Vector
        }

        public void FixedUpdate()
        {
            _movementVector = Vector3.zero;

            Velocity(); // Calculate Current Velocity

            Grounded();

            Movement();

            Gravity();

            _transform.position += _movementVector * Time.fixedDeltaTime;
        }

        private void Velocity()
        {
            _velocity = _transform.position - _prevPosition;
            _prevPosition = _transform.position;
            Debug.Log(_velocity.sqrMagnitude * (1/Time.fixedDeltaTime));
        }
        private void Grounded()
        {
            if (Physics.Raycast(_transform.position + _groundCastOffset, -_transform.up, out _groundHit, _groundCastDistance, (1 << 8) | (1 << 9))) // Check if Grounded
            {
                _isGrounded = true;
            }
            else
            {
                _isGrounded = false;
            }
        }
        private void Movement()
        {
            // Damping
            MathfUtility.LinearDampTowardZero(ref _currentMovement.y, _movementDamp.y * Time.fixedDeltaTime);
            MathfUtility.LinearDampTowardZero(ref _currentMovement.x, _movementDamp.x * Time.fixedDeltaTime);
            // Move if Grounded
            if (_isGrounded)
            {
                _currentMovement.y += _movementInput.y * _movementAcc.y * Time.fixedDeltaTime;
                _currentMovement.x += _movementInput.x * _movementAcc.x * Time.fixedDeltaTime;

                _currentMovement.y = Mathf.Clamp(_currentMovement.y, _movementClampForward.x, _movementClampForward.y);
                _currentMovement.x = Mathf.Clamp(_currentMovement.x, _movementClampRight.x, _movementClampRight.y);


                _movementVector += _transform.forward * _currentMovement.y + _transform.right * _currentMovement.x;
            }
        }
        private void Gravity()
        {
            if (_useGravity)
            {
                if (_isGrounded)
                {
                    _currentGravity = Vector3.zero; // Reset Gravity Vector
                    /*
                    if (_groundHit.distance < _groundCastDistance)
                    {
                        _transform.position += _transform.up * (_groundCastDistance - _groundHit.distance);
                    }
                    */
                }
                else
                {
                    _currentGravity += _gravity * Time.fixedDeltaTime; // Gain Gravity
                    _movementVector += _currentGravity;
                }
            }
        }


    }
}
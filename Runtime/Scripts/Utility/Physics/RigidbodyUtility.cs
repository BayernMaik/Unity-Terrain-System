using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RigidbodyUtility
{
    // Velocity
    /// <summary>
    /// Get the Rigidbodys velocity along its transform attached forward direction
    /// </summary>
    /// <param name="rigidbody"></param>
    /// <returns></returns>
    public static float VelocityForward(this Rigidbody rigidbody)
    {
        if (Vector3.Dot(rigidbody.velocity, rigidbody.transform.forward) < 0.0f)
        {
            return -(rigidbody.velocity * Vector3.Dot(rigidbody.velocity.normalized, rigidbody.transform.forward)).magnitude;
        }
        else
        {
            return (rigidbody.velocity * Vector3.Dot(rigidbody.velocity.normalized, rigidbody.transform.forward)).magnitude;
        }
    }
    public static float AxisVelocity(this Rigidbody rigidbody, Vector3 velocity, Vector3 axis)
    {
        if (Vector3.Dot(velocity, axis) < 0.0f)
        {
            return -(velocity * Vector3.Dot(velocity.normalized, axis.normalized)).magnitude;
        }
        else
        {
            return (velocity * Vector3.Dot(velocity.normalized, axis.normalized)).magnitude;
        }
    }
    public static float AbsoluteVelocityForward(this Rigidbody rigidbody)
    {
        return (rigidbody.velocity * Vector3.Dot(rigidbody.velocity.normalized, rigidbody.transform.forward)).magnitude;
    }
    /// <summary>
    /// Get the Rigidbodys velocity along its transform attached right direction
    /// </summary>
    /// <param name="rigidbody"></param>
    /// <returns></returns>
    public static float VelocityRight(this Rigidbody rigidbody)
    {
        if (Vector3.Dot(rigidbody.velocity, rigidbody.transform.right) < 0)
        {
            return -(rigidbody.velocity * Vector3.Dot(rigidbody.velocity.normalized, rigidbody.transform.right)).magnitude;
        }
        else
        {
            return (rigidbody.velocity * Vector3.Dot(rigidbody.velocity.normalized, rigidbody.transform.right)).magnitude;
        }
    }
    public static float AbsoluteVelocityRight(this Rigidbody rigidbody)
    {
        return (rigidbody.velocity * Vector3.Dot(rigidbody.velocity.normalized, rigidbody.transform.right)).magnitude;
    }
    /// <summary>
    /// Get the Rigidbodys velocity along its attached transform right direction
    /// </summary>
    /// <param name="rigidbody"></param>
    /// <returns></returns>
    public static float VelocityUp(this Rigidbody rigidbody)
    {
        return (rigidbody.velocity * Vector3.Dot(rigidbody.velocity.normalized, rigidbody.transform.up)).magnitude;
    }

    public static bool IsMoving(this Rigidbody rigidbody, float threshold = 0.000001f)
    {
        if (rigidbody.velocity.sqrMagnitude < threshold)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public static bool IsGrounded(this Rigidbody rigidbody, Vector3 offset, ref RaycastHit raycastHit, float distance, int layer)
    {
        if (Physics.Raycast(rigidbody.position + offset, -rigidbody.transform.up, out raycastHit, distance, layer)) // Check if Grounded
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

public class RigidbodyController
{
    private int _terrainLayer = (1 << 8);
    private int _terrainObjectLayer = (1 << 9);
    private Rigidbody _rigidbody;
    #region Movement
    private Vector3 _movementVector, _forwardVector, _rightVector;
    private Vector3 _currentMovementSpeed;
    private Vector2 _movementInput, _currentMovement;
    private Vector2 _movementAcc = new Vector2(10.0f, 10.0f), _movementDamp = new Vector2(5.0f, 5.0f);
    private Vector2 _movementClampForward = new Vector2(-2.25f, 2.25f), _movementClampRight = new Vector2(-2.25f, 2.25f);
    public Vector2 movementInput { get { return _movementInput; } set { _movementInput = value; } }
    #endregion
    #region Gravity
    private bool _useGravity = true;
    private Vector3 _gravity, _currentGravity;
    public bool useGravity { get { return _useGravity; } set { _useGravity = value; } }
    public Vector3 gravity { get { return _gravity; } set { _gravity = value; } }
    #endregion
    #region Grounded
    private bool _isGrounded = false;
    private Vector3 _groundCastOffset = new Vector3(0.0f, 0.5f, 0.0f);
    private Vector3 _slopeForward = Vector3.zero, _slopeRight = Vector3.zero;
    private float _slopeForwardAngle = 0.0f, _slopeRightAngle = 0.0f;
    private float _slopeForwardRange = 30.0f, _slopeRightRange = 30.0f;
    private float _groundCastDistance = 0.5f, _slopeAngle = 0.0f;
    private RaycastHit _groundHit;
    public Vector3 groundCastOffset { get { return _groundCastOffset; } set { _groundCastOffset = value; } }
    #endregion
    #region Velocity
    private Vector3 _velocity;
    private Vector3 _prevPosition;
    public Vector3 velocityVector { get { return _velocity; } }
    #endregion
    
    public RigidbodyController(Rigidbody rigidbody)
    {
        this._rigidbody = rigidbody;
        this._prevPosition = rigidbody.position;
        this._gravity = Physics.gravity; // Set default Gravity Vector to Physics Gravity Vector
    }

    public void FixedUpdate()
    {
        _movementVector = Vector3.zero;

        Velocity(); // Calculate Current Velocity

        Grounded();

        Move();

        Gravity();

        _rigidbody.MovePosition(_rigidbody.position + _movementVector * Time.fixedDeltaTime);
    }

    private void Velocity()
    {
        _velocity = _rigidbody.position - _prevPosition;
        _prevPosition = _rigidbody.position;
        //Debug.Log((_velocity / Time.fixedDeltaTime).magnitude);
    }
    private void Grounded()
    {
        if (Physics.Raycast(_rigidbody.position + _groundCastOffset, -_rigidbody.transform.up, out _groundHit, _groundCastDistance, ~(1 << 6))) // Check if Grounded
        {
            // Set Movement Direction Depending on Surface and Object
            if (_groundHit.transform.gameObject.tag == "Walkable")
            {
                _forwardVector = _rigidbody.transform.forward;
                _rightVector = _rigidbody.transform.right;
            }
            else
            {
                _forwardVector = Vector3.ProjectOnPlane(_rigidbody.transform.forward, _groundHit.normal);
                _forwardVector = Quaternion.AngleAxis(Vector3.SignedAngle(_forwardVector, _rigidbody.transform.forward, Vector3.up), Vector3.up) * _forwardVector;  // Correct Direction Shifts on Spherical Obstacles
                _slopeForwardAngle = Vector3.Angle(_rigidbody.transform.forward, _slopeForward);
                //Debug.DrawRay(_rigidbody.position, _forwardVector, Color.green);

                _rightVector = Vector3.ProjectOnPlane(_rigidbody.transform.right, _groundHit.normal);
                _rightVector = Quaternion.AngleAxis(Vector3.SignedAngle(_rightVector, _rigidbody.transform.right, Vector3.up), Vector3.up) * _rightVector; // Correct Direction Shifts on Spherical Obstacles
                _slopeRightAngle = Vector3.Angle(_rigidbody.transform.right, _slopeRight);
                //Debug.DrawRay(_rigidbody.position, _rightVector, Color.green);
            }
            _isGrounded = true;
        }
        else
        {
            //Debug.DrawRay(_rigidbody.position + _groundCastOffset, -_rigidbody.transform.up * _groundCastDistance, Color.red);
            _isGrounded = false;
        }
    }
    private void Move()
    {
        // Damping
        MathfUtility.LinearDampTowardZero(ref _currentMovement.y, _movementDamp.y * Time.fixedDeltaTime);
        MathfUtility.LinearDampTowardZero(ref _currentMovement.x, _movementDamp.x * Time.fixedDeltaTime);
        // Move if Grounded
        if (_isGrounded)
        {
            // Increase
            _currentMovement.y += _movementInput.y * _movementAcc.y * Time.fixedDeltaTime;
            _currentMovement.x += _movementInput.x * _movementAcc.x * Time.fixedDeltaTime;
            // Clamp
            _currentMovement.y = Mathf.Clamp(_currentMovement.y, _movementClampForward.x, _movementClampForward.y);
            _currentMovement.x = Mathf.Clamp(_currentMovement.x, _movementClampRight.x, _movementClampRight.y);
            // Normalize
        }

        _movementVector += _forwardVector * _currentMovement.y + _rightVector * _currentMovement.x;
    }
    private void Gravity()
    {
        if (_useGravity)
        {
            if (_isGrounded)
            {
                _currentGravity = Vector3.zero; // Reset Gravity Vector

                if (_groundHit.transform.gameObject.tag == "Walkable")
                {
                    _rigidbody.MovePosition(_rigidbody.position + _rigidbody.transform.up * Mathf.Min(0.25f * Time.fixedDeltaTime, _groundCastDistance - _groundHit.distance));
                    //_currentGravity += _gravity * Time.fixedDeltaTime; // Gain Gravity
                    //_movementVector += _currentGravity;
                }
                /*
                if (_groundHit.distance < _groundCastDistance)
                {
                    _rigidbody.MovePosition(_rigidbody.position + _rigidbody.transform.up * Mathf.Min(0.25f * Time.fixedDeltaTime, _groundCastDistance - _groundHit.distance));
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
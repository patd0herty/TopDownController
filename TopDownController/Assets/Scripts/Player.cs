using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

[SelectionBase]
public class Player : MonoBehaviour
{

    #region Fields
    [SerializeField] private float _moveSpeed = 50f;

    [SerializeField] private float _dashSpeedMultiplier = 2f;

    [SerializeField] private Rigidbody2D _rb;

    private Vector2 _moveDirect = Vector2.zero;

    private SpriteRenderer _spriteRenderer;

    private Animator _animator;

    private InputAction _dash;

    public const float DASH_DURATION = 0.5f;

    private float _dashTimer = 0.0f;

    private bool _isDashing = false;

    //private  _controls;
    #endregion

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponentInChildren<Animator>();
        _animator.SetFloat("xVelocity", 0.0f);
        _animator.SetFloat("yVelocity", 0.0f);
        _animator.SetBool("xEqualZero", true);
        _animator.SetBool("yEqualZero", true);


        
    }

    #region Loop
    private void Update()
    {
        // Dash timer stuff
        if (_isDashing)
        {
            _dashTimer -= Time.deltaTime;
            if (_dashTimer <= 0.0f)
            {
                _dashTimer = 0.0f;
                _isDashing = false;
            }
        }

        GatherInput();
        _walkAnimation();
    }

    private void FixedUpdate()
    {
        MovementUpdate();
    }
    #endregion

    #region Input Logic
    private void GatherInput()
    {
        _moveDirect.x = Input.GetAxisRaw("Horizontal");
        _moveDirect.y = Input.GetAxisRaw("Vertical");

        _moveDirect.Normalize();

        print(_moveDirect);

        if (Input.GetKeyDown(KeyCode.Space))
            DashPressed();
    }
    #endregion

    #region Movement
    private void MovementUpdate()
    {
        _rb.linearVelocity = _moveDirect * _moveSpeed * Time.fixedDeltaTime;
        if (_isDashing)
        {
            // Scale the velocity by the speed multiplier while dashing
            _rb.linearVelocity = _rb.linearVelocity * _dashSpeedMultiplier;
        }
    }

    #endregion

    #region HelperMethods
    private void _walkAnimation()
    {
        _animator.SetFloat("xVelocity", _moveDirect.x);
        _animator.SetFloat("yVelocity", _moveDirect.y);
        // Check if X direction is exactly zero and update animator bools accordingly
        if(_moveDirect.x == 0)
        {
            _animator.SetBool("xEqualZero", true);
        }
        else
        {
            _animator.SetBool("xEqualZero", false);
        }
        // Check if y direction is exactly zero and update animator bools accordingly
        if (_moveDirect.y == 0)
        {
            _animator.SetBool("yEqualZero", true);
        }
        else
        {
            _animator.SetBool("yEqualZero", false);
        }
    }
    #endregion

    #region Events(Kinda)

    public void DashPressed()//InputAction.CallbackContext ctx)
    {
        if (!_isDashing)
        {
            _isDashing = true;
            _dashTimer = DASH_DURATION;
        }
    }

    #endregion
}

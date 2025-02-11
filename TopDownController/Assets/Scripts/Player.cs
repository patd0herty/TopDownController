using UnityEngine;

[SelectionBase]
public class Player : MonoBehaviour
{

    #region Fields
    [SerializeField] private float _moveSpeed = 50f;

    [SerializeField] private Rigidbody2D _rb;

    private Vector2 _moveDirect = Vector2.zero;

    private SpriteRenderer _spriteRenderer;

    private Animator _animator;
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
    }
    #endregion

    #region Movement
    private void MovementUpdate()
    {
        _rb.linearVelocity = _moveDirect * _moveSpeed * Time.fixedDeltaTime;
        
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

    private string _getDirection()
    {

        // if something goes wrong default to the down animationn
        return "down";
    }
    #endregion
}

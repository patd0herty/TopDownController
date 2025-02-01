using UnityEngine;

[SelectionBase]
public class Player : MonoBehaviour
{

    #region Fields
    [SerializeField] private float _moveSpeed = 50f;

    [SerializeField] private Rigidbody2D _rb;

    private Vector2 _moveDirect = Vector2.zero;
    #endregion

    #region Tick
    private void Update()
    {
        GatherInput();
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
}

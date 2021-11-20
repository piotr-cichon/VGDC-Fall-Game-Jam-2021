using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Movement : MonoBehaviour
{
    [Header("Ground check")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    
    [Header("Movement speeds")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpSpeed;
    
    [SerializeField]
    private bool _isGrounded;

    private bool _jumping;
    private bool _faceRight = true;
    private Rigidbody2D _rb;
    private float _horizontalMovement;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _horizontalMovement = Input.GetAxisRaw("Horizontal");
        _isGrounded = CheckGround();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _jumping = true;
            Jump();
        }
    }

    bool CheckGround()
    {
        var colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius, groundLayer);
        if (colliders.Length > 0)
            return true;
        return false;
    }

    private void Jump()
    {
        print("Jump");
        _rb.AddForce(Vector2.up * jumpSpeed,ForceMode2D.Impulse);
    }

    private void ChangeOrientation(float movX)
    {
        if (_faceRight && movX < 0)
        {
            _faceRight = false;
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }else if (_faceRight == false && movX > 0)
        {
            _faceRight = true;
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }
    }

    private void FixedUpdate()
    {
        print(_horizontalMovement);
        float movX = _horizontalMovement * moveSpeed;
        ChangeOrientation(movX);
        _rb.velocity = new Vector2(movX, _rb.velocity.y);
        
    }
}

using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Movement : MonoBehaviour
{
    [Header("Ground check")] [SerializeField]
    private LayerMask groundLayer;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;

    [Header("Movement speeds")]
    [SerializeField] private float moveSpeed = 5;

    [SerializeField] private float jumpSpeed = 6;
    public bool doubleJump = false;


    private bool _isGrounded;
    private int _jumpCount;
    private bool _jumping;

    private bool _faceRight = true;
    private float _horizontalMovement;
    
    private Rigidbody2D _rb;
    private Animator _animator;
    private SpriteRenderer _sprite;
    
    private static readonly int Moving = Animator.StringToHash("moving");
    private static readonly int YVelocity = Animator.StringToHash("yVelocity");
    private static readonly int JumpBool = Animator.StringToHash("jump");

    public void SetDoubleJump()
    {
        doubleJump = true;
    }
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
    }
    

    void Update()
    {
        _horizontalMovement = Input.GetAxisRaw("Horizontal");
        _isGrounded = CheckGround();

        if (((_isGrounded || ( doubleJump && _jumpCount < 2 && _rb.velocity.y > -0.1f)) &&
            Input.GetKeyDown(KeyCode.Space)))
        {
            _jumpCount++;
            _jumping = true;
            _animator.SetBool(JumpBool, true);
            Jump();
        }

        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        // if is grounded and jumping is true and velocity is pointed down
        if (_isGrounded && _jumping && _rb.velocity.y < 0)
        {
            _jumping = false;
            _jumpCount = 0;
            _animator.SetBool(JumpBool, false);
        }

        if (_isGrounded)
        {
            _animator.SetBool(Moving, _horizontalMovement != 0);
        }
        else
        {
            _animator.SetFloat(YVelocity, _rb.velocity.y);
            _animator.SetBool(Moving, false);
        }
    }

    private bool CheckGround()
    {
        var colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius, groundLayer);
        if (colliders.Length > 0)
            return true;
        return false;
    }

    private void Jump()
    {
        _rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
    }

    private void FlipSprite()
    {
        _sprite.flipX = !_sprite.flipX;
    }
    private void ChangeOrientation(float movX)
    {
        if (_faceRight && movX < 0)
        {
            _faceRight = false;
            FlipSprite();
        }
        else if (_faceRight == false && movX > 0)
        {
            _faceRight = true;
            FlipSprite();
        }
    }

    private void FixedUpdate()
    {
        float movX = _horizontalMovement * moveSpeed;
        ChangeOrientation(movX);
        _rb.velocity = new Vector2(movX, _rb.velocity.y);
    }
}
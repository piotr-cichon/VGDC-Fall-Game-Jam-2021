using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float initialSpeed = 300;
    [SerializeField] private float increment = 100;
    [SerializeField] private float maxSpeed = 600;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform shootingTransformRight;
    [SerializeField] private Transform shootingTransformLeft;
    [SerializeField] private int hp;

    [SerializeField] private float defaultXForce = 100;
    private float _xForce;
    private SpriteRenderer _sprite;
    private Transform _shootingTransform;
    private Movement _movement;
    private float _ballThrowPower;

    private void Awake()
    {
        _movement = GetComponent<Movement>();
        _sprite = GetComponent<SpriteRenderer>();
        _ballThrowPower = initialSpeed;
        _xForce = defaultXForce;
    }
    
    public void EnableDoubleJump()
    {
        _movement.SetDoubleJump();
    }

    public void Update()
    {
        if (_sprite.flipX)
        {
            _shootingTransform = shootingTransformLeft;
            _xForce = -defaultXForce;
        }
        else
        {
            _shootingTransform = shootingTransformRight;
            _xForce = defaultXForce;
        }

        if (Input.GetButton("Fire1"))
        {
            _ballThrowPower += increment * Time.deltaTime;
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            GameObject ball = Instantiate(bullet, null, true);
            ball.transform.position = _shootingTransform.position;
            
            ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(_xForce,_ballThrowPower),ForceMode2D.Force);
            ball.GetComponent<Ball>().SetPlayer(this);
            _ballThrowPower = initialSpeed;
        }
    }

    public void DecrementHp()
    {
        hp--;
        if (hp == 0)
        {
            Destroy(this.gameObject);
        }
    }
}

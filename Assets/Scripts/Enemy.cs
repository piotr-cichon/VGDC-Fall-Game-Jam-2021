using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor.Animations;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int deviation;
    [SerializeField] private double height;
    [SerializeField] private AnimatorOverrideController petAnimator;


    private Animator _animator;
    private Movement _movement;
    private Vector2 _initialPosition;
    private int _direction;
    private SpriteRenderer _spriteRenderer;
    private bool _pet;
    
    void Start()
    {
        _initialPosition = transform.position;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _movement = GetComponent<Movement>();
        _animator = GetComponent<Animator>();
        _direction = 1;
    }
    
    void Update()
    {
        transform.position += new Vector3(_direction, 0,0) * Time.deltaTime;
        if (!_pet)
        {
            if (_initialPosition.x + deviation <= transform.position.x)
            {
                _spriteRenderer.flipX = true;
                _direction = -1;
            }
            else if (_initialPosition.x >= transform.position.x)
            {
                _spriteRenderer.flipX = false;
                _direction = 1;
            }
        }
        else
        {
            
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.GetComponent<Player>() != null)
        {
            Vector3 playerPosition = other.collider.GetComponent<Transform>().position;
            Vector3 enemyPosition = transform.position;
            if (playerPosition.y - height >= enemyPosition.y)
            {   
                Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
                rb.freezeRotation = true;
                _direction = 0;
                _pet = true;
                _spriteRenderer.flipX = other.gameObject.GetComponent<SpriteRenderer>().flipX;
                _movement.enabled = true;
                _animator.runtimeAnimatorController = petAnimator;
                Destroy(this);
            }
        }
    }
}

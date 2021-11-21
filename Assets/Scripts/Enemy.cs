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
    [SerializeField] private bool canBePetted = true;
    [SerializeField] private AnimatorOverrideController petAnimator;
    
    [SerializeField] private SpriteRenderer selectedImage;
    [SerializeField] private SpriteRenderer isAPet;

    private Animator _animator;
    private Movement _movement;
    private Vector2 _initialPosition;
    private int _direction;
    private SpriteRenderer _spriteRenderer;
    private bool _pet;

    void Start()
    {
        isAPet.enabled = false;
        selectedImage.enabled = false;
        
        _initialPosition = transform.position;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _movement = GetComponent<Movement>();
        _animator = GetComponent<Animator>();
        _direction = 1;
    }

    void Update()
    {
        transform.position += new Vector3(_direction, 0, 0) * Time.deltaTime;
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

    public void ActivatePet()
    {
        isAPet.enabled = false;
        _movement.enabled = true;
        selectedImage.enabled = true;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public void DisablePet()
    {
        GetComponent<Rigidbody2D>().constraints |= RigidbodyConstraints2D.FreezePositionX;
        isAPet.enabled = true;
        _movement.enabled = false;
        selectedImage.enabled = false;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (canBePetted && !_pet && other.collider.GetComponent<Player>() != null)
        {
            Player player = other.collider.GetComponent<Player>();
            Vector3 playerPosition = other.transform.position;
            Vector3 enemyPosition = transform.position;
            if (playerPosition.y - height >= enemyPosition.y)
            {
                isAPet.enabled = true;
                if(gameObject.GetComponent<Rigidbody2D>() == null)
                    gameObject.AddComponent<Rigidbody2D>();
                Debug.Log(name + " is a pet right now");
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                rb.constraints |= RigidbodyConstraints2D.FreezePositionX;
                GetComponent<SpriteRenderer>().flipX = false;
                rb.freezeRotation = true;
                _pet = true;
                _animator.runtimeAnimatorController = petAnimator;
                player.AddPetEnemy(this);
                this.enabled = false; // disable the script
            }
        }
    }
}
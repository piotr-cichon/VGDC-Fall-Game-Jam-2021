using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private KeyCode changePet = KeyCode.C;
    [SerializeField] private KeyCode resetPlayer = KeyCode.P;
    
    [SerializeField] private float initialSpeed = 300;
    [SerializeField] private float increment = 100;
    [SerializeField] private float maxSpeed = 600;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform shootingTransformRight;
    [SerializeField] private Transform shootingTransformLeft;

    [SerializeField] private float defaultXForce = 100;

    private List<Enemy> _pets = new List<Enemy>();
    private Enemy _previoursPet;
    private int _petIndex = 0;
    
    private float _xForce;
    private SpriteRenderer _sprite;
    private Movement _movement;
    
    private Transform _shootingTransform;
    private float _ballThrowPower;
    private Ball _ballThrown = null;
    
    private bool _useTeleport;

    private CinemachineVirtualCamera _cinemachine;

    private void Awake()
    {
        _movement = GetComponent<Movement>();
        _sprite = GetComponent<SpriteRenderer>();
        _ballThrowPower = initialSpeed;
        _xForce = defaultXForce;
        
        _cinemachine = FindObjectOfType<CinemachineVirtualCamera>();
    }

    public void AddPetEnemy(Enemy enemy)
    {
        _pets.Add(enemy);
    }
    
    public void EnableDoubleJump()
    {
        _movement.doubleJump = true;
        _useTeleport = false;
    }

    public void EnableTeleport()
    {
        _movement.doubleJump = false;
        _useTeleport = true;
    }

    private void TeleportBall()
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
        else if (Input.GetButtonUp("Fire1") && _ballThrown == null)
        {
            GameObject ball = Instantiate(bullet, null, true);
            _ballThrown = ball.GetComponent<Ball>();
            _ballThrown.transform.position = _shootingTransform.position;
            _ballThrown.GetComponent<Rigidbody2D>().AddForce(new Vector2(_xForce,_ballThrowPower),ForceMode2D.Force);
            _ballThrown.SetPlayer(this);
            _ballThrowPower = initialSpeed;
        }
    }

    private void EnablePlayer()
    {
        _petIndex = 0;
        _cinemachine.Follow = transform;
        _movement.enabled = true;
    }

    private void DisablePlayer()
    {
        _movement.enabled = false;
    }
    public void Update()
    {
        if(_useTeleport)
            TeleportBall();
        if (Input.GetKeyUp(changePet))
        {
            if(_previoursPet != null)
                _previoursPet.DisablePet();
            if (_petIndex < _pets.Count)
            {
                DisablePlayer();
                Enemy enemy = _pets[_petIndex];
                ++_petIndex;
                enemy.ActivatePet();
                _cinemachine.Follow = enemy.transform;
                _previoursPet = enemy;
            }
            else
            {
                // enable player
                EnablePlayer();
            }
        }

        if (Input.GetKeyUp(resetPlayer))
        {
            if(_previoursPet != null)
                _previoursPet.DisablePet();
            EnablePlayer();
        }
    }
}

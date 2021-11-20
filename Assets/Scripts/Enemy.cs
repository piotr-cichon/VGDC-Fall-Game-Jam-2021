using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int deviation;
    [SerializeField] private double height;
    private Vector2 initialPosition;
    private int direction;
    private SpriteRenderer spriteRenderer;
    private bool pet;
    
    void Start()
    {
        initialPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        direction = 1;
    }
    
    void Update()
    {
        transform.position += new Vector3(direction, 0,0) * Time.deltaTime;
        if (!pet)
        {
            if (initialPosition.x + deviation <= transform.position.x)
            {
                spriteRenderer.flipX = true;
                direction = -1;
            }
            else if (initialPosition.x >= transform.position.x)
            {
                spriteRenderer.flipX = false;
                direction = 1;
            }
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
                direction = 0;
                pet = true;
            }
        }
    }
}

using System;
using UnityEngine;

public class DoubleJumpPowerup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>() != null)
        {
            other.GetComponent<Player>().EnableDoubleJump();
            Destroy(gameObject);
        }
    }
}
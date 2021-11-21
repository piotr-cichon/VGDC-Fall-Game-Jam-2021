using System;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Player _player;

    public void SetPlayer(Player player)
    {
        _player = player;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (_player != null)
        {
            if (other.transform.GetComponent<Fruit>() != null) return;
            _player.transform.position = other.contacts[0].point + new Vector2(0,2f);
            Destroy(gameObject);
        }
    }
}
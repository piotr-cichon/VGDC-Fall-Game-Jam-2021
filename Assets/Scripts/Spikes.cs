using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] private float waitTime = 1f;
    private bool _decrement = false;
    private void Start()
    {

    }

    private IEnumerator ResetDecrement(GameObject player)
    {
        yield return new WaitForSeconds(waitTime);
        _decrement = false;
        if(player != null)
            player.GetComponent<SpriteRenderer>().color = Color.white;

    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.collider.GetComponent<Player>() != null && !_decrement)
        {
            _decrement = true;
            other.gameObject.GetComponent<Player>().DecrementHp();
            other.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            StartCoroutine(ResetDecrement(other.gameObject));
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        print("exit collision spike");
    }
}

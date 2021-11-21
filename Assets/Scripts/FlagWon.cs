using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlagWon : MonoBehaviour
{
    [SerializeField] private float yForce = 5;
    [SerializeField] private AnimationClip roll;
    private Player _player;
    private Rigidbody2D _rigidbody;
    private bool _animFinished = false;
    private static readonly int Roll = Animator.StringToHash("roll");
    private bool _started = false;
    
    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(roll.length + 0.1f);
        _animFinished = true;
    }

    private void Update()
    {
        if (_player != null &&  _animFinished && _player.OnGround())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
         _player = other.GetComponent<Player>();
        if (_player != null && !_started)
        {
            _started = true;
            _player.GetComponent<Movement>().enabled = false;
            _rigidbody = _player.GetComponent<Rigidbody2D>();
            _rigidbody.AddForce(new Vector2(0,yForce),ForceMode2D.Impulse);
            Animator animator = _player.GetComponent<Animator>();
            animator.SetTrigger(Roll);
            StartCoroutine(Wait());
        }
    }
}
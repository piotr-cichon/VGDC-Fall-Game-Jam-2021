using System;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private bool canBeOnTriggerEnted = false;
    [SerializeField] private float speed = 1.5f;
    [SerializeField] private float length = 3f;
    [SerializeField] private bool useLengthOffsed = false;

    [SerializeField] private bool startLeft = true;
    [SerializeField] private Transform positionLeft;
    [SerializeField] private Transform positionRight;

    private Vector3 _nextPos;
    private Vector3 _startPos;
    private Vector3 _endPos;

    private void AdjustLength(ref Vector3 v)
    {
        if (useLengthOffsed)
            v -= new Vector3(length, 0, 0);
    }

    void Awake()
    {
        if (startLeft)
        {
            _startPos = positionLeft.position;
            _endPos = positionRight.position;
            AdjustLength(ref _endPos);
        }
        else
        {
            _startPos = positionRight.position;
            AdjustLength(ref _startPos);
            _endPos = positionLeft.position;
        }

        _nextPos = _startPos;
    }

    void Update()
    {
        if (transform.position == _startPos)
            _nextPos = _endPos;
        if (transform.position == _endPos)
            _nextPos = _startPos;
        transform.position = Vector3.MoveTowards(transform.position, _nextPos, Time.deltaTime * speed);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(positionLeft.position, 0.1f);
        Gizmos.DrawSphere(positionRight.position, 0.1f);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(positionLeft.position, positionRight.position);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if (player != null || enemy != null)
        {
            other.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if (player != null || enemy != null)
        {
            other.transform.SetParent(null);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        print("Collision here with platform on trigger enter" + other.gameObject.name);
        Player player = other.gameObject.GetComponent<Player>();
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if (player != null || enemy != null)
        {
            other.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if (player != null || enemy != null)
        {
            other.transform.SetParent(null);
        }
    }
}
using System;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float speed = 1.5f;
    [SerializeField] private float maxDistance;
    [Tooltip("It increments that vector every frame")]
    [SerializeField] private Vector3 displacement;

    private bool _movingAway = true;
    private Vector3 _startPosition;
    void Awake()
    {
        _startPosition = transform.position;
        
    }

    void Update()
    {
        transform.position += displacement * Time.deltaTime * speed;
        Vector3 direction = transform.position - _startPosition;
        if (Vector3.SqrMagnitude(direction) > maxDistance * maxDistance && _movingAway)
        {
            _movingAway = false;
            displacement *= -1;
        }
        if (_movingAway == false && Vector3.SqrMagnitude(direction) < 0.1f)
        {
            _movingAway = true;
            displacement *= -1;
        }
    }

    private void OnDrawGizmos()
    {   
        if(Application.isPlaying == false)
            _startPosition = transform.position;
        Gizmos.color = Color.red;
        Vector3 displace = new Vector3(0, 1f, 0);
        Vector3 start = _startPosition + displace;
        Vector3 end = start + displacement * maxDistance;
        Gizmos.DrawSphere(start ,0.2f);
        Gizmos.DrawSphere(end,0.2f);
        Gizmos.DrawLine(start,end);
    }
}
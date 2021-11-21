using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FallingRock : MonoBehaviour
{
    [SerializeField] private float playerHeight = 2f;
    [SerializeField] private LayerMask ground;
    [SerializeField] private Transform groundCheck;
    // Start is called before the first frame update
    [SerializeField] private float initialRockSpeedY = -10f;
    [SerializeField] private float initialRockAccelerationY = -0.1f;
    private float _rockAccelerationY, _rockSpeedY;
    private Vector3 _rockSpeed, _initialPosition;
    private bool _hasStartedCoroutine = false;
    private Animator rockAnimator;
    private static readonly int OnIdle = Animator.StringToHash("OnIdle");
    private static readonly int OnHitBottom = Animator.StringToHash("OnHitBottom");
    private static readonly int OnFall = Animator.StringToHash("OnFall");

    private void Awake()
    {
        rockAnimator = GetComponent<Animator>();
        _rockSpeedY = initialRockSpeedY;
        _rockAccelerationY = initialRockAccelerationY;
        _rockSpeed = new Vector3(0f, _rockSpeedY, 0f);
        _initialPosition = transform.position;
    }

    void Start()
    {
        rockAnimator.SetBool(OnIdle, false);
        rockAnimator.SetBool(OnHitBottom, false);
        rockAnimator.SetBool(OnFall, true);
    }

    IEnumerator WaitRock(int numberSeconds, float newAcceleration, float newSpeedY, bool onFallNew, bool onHitBottomNew)
    {
        _rockAccelerationY = 0f;
        _rockSpeed.y = 0f;
        yield return new WaitForSeconds(numberSeconds);
        _rockSpeed.y = newSpeedY;
        _rockAccelerationY = newAcceleration;
        rockAnimator.SetBool(OnHitBottom, onHitBottomNew);
        rockAnimator.SetBool(OnFall, onFallNew);
        print("Am iesit din corutina");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Fruit fruit = other.GetComponent<Fruit>();
        Player player = other.GetComponent<Player>();
        if (fruit != null)
        {
            Destroy(fruit.gameObject);
            return;
        }
        if (player != null)
        {
            if (transform.position.y > player.transform.position.y)
            {
                player.Die();
                print("Hit player");
            }

        }
        else
        {
            rockAnimator.SetBool(OnFall, false);
            rockAnimator.SetBool(OnHitBottom, true);
            StartCoroutine(WaitRock(2, 0f, -initialRockSpeedY, false, true));
            _hasStartedCoroutine = false;
            print("Am intrat in trigger enter");
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        rockAnimator.SetBool(OnHitBottom, false);
    }

    // Update is called once per frame
    void Update()
    {
        _rockSpeed.y += _rockAccelerationY;
        transform.position += _rockSpeed * Time.deltaTime;

        if (_hasStartedCoroutine == false && transform.position.y >= _initialPosition.y)
        {
            rockAnimator.SetBool(OnHitBottom, false);
            rockAnimator.SetBool(OnFall, false);
            _hasStartedCoroutine = true;
            StartCoroutine(WaitRock(2, initialRockAccelerationY, initialRockSpeedY, true, false));
        }
    }
}

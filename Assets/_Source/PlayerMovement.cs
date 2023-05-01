using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    private bool _isRunning = false;
    [SerializeField] private float _currentSpeed;
    [SerializeField] private float _accelerationPerSecond;

    [SerializeField] private float _jerkSpeedMultyplier;
    [SerializeField] private float _jerkDuration;

    [SerializeField] private LayerMask _obstacleLayer;

    [SerializeField] private UIManager _uiManager;

    [SerializeField] private AnimationCurve _jumpCurve;
    [SerializeField] private AnimationCurve _actionDurationToSpeed;

    [SerializeField] private float _jumpDuration;
    private float _jumpTimer = 0;
    private bool _isJumping = false;

    private bool _isJerking = false;

    private bool _canAct = false;

    [SerializeField] private float _runDelay;

    [SerializeField] private Animator _animator;

    public void Start()
    {
    }

    private void Update()
    {
        if (_canAct)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && !_isJumping && !_isJerking)
            {
                _isJumping = true;
                _jumpDuration = _actionDurationToSpeed.Evaluate(_currentSpeed);
            }

            if (Input.GetKeyDown(KeyCode.RightArrow) && !_isJumping && !_isJerking)
                MakeJerk();
        }

        if (_isRunning)
        {
            transform.position = new Vector2(transform.position.x + _currentSpeed * Time.deltaTime, transform.position.y);
            _currentSpeed += _accelerationPerSecond * Time.deltaTime;
        }

        if (_isJumping)
        {
            _jumpTimer += Time.deltaTime;
            transform.position = new Vector2(transform.position.x, _jumpCurve.Evaluate(_jumpTimer / _jumpDuration));
            if (_jumpTimer > _jumpDuration)
            {
                _isJumping = false;
                _jumpTimer = 0;
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision");
        if ((_obstacleLayer & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
            Die();
    }

    public void StartMoving()
    {
        _isRunning = true;
        Invoke("UnlockMovement", _runDelay);
    }

    private void UnlockMovement()
    {
        _canAct = true;
    }

    private void MakeJerk()
    {
        _isJerking = true;
        Physics2D.IgnoreLayerCollision(6, 7, true);
        Invoke("FinishJerk", _actionDurationToSpeed.Evaluate(_currentSpeed));
    }

    private void FinishJerk()
    {
        _isJerking = false;
        Physics2D.IgnoreLayerCollision(6, 7, false);
    }

    private void Die()
    {
        _uiManager.OnDeath();
        _isRunning = false;
    }

    public void BlockMovement()
    {
        _canAct = false;
        _currentSpeed = 4;
    }
}

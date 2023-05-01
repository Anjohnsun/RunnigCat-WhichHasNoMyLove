using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float _xOffset;
    [SerializeField] private Transform _player;
    [SerializeField] private float _followIntensity;

    [SerializeField] private float _softMovementDuration;

    private bool _softMovementEnabled = true;

    private void Start()
    {
        Invoke("SetSoftMovementOff", _softMovementDuration);
    }

    void Update()
    {
        if (_softMovementEnabled)
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, _player.position.x + _xOffset, _followIntensity), transform.position.y, -10);
        else
            transform.position = new Vector3(_player.position.x + _xOffset, transform.position.y, -10);
    }

    private void SetSoftMovementOff()
    {
        _softMovementEnabled = false;
    }
}

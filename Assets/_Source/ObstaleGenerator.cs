using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaleGenerator : MonoBehaviour
{
    [SerializeField] private float _generateDelay;
    [SerializeField] private List<GameObject> _obstacles;
    [SerializeField] private Transform _player;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private LayerMask _obstacleLayer;
    [SerializeField] private LayerMask _endLocationLayer;
    [SerializeField] private float _offset;

    [SerializeField] private GameObject _endLocation;
    [SerializeField] private float _levelDuration;

    private bool _continueSpawning = true;

    public IEnumerator CreateAnObstacle(float delay)
    {
        yield return new WaitForSeconds(delay);
        Instantiate(_obstacles[Random.Range(0, _obstacles.Count)], new Vector2(_player.position.x + _offset, 0), new Quaternion());
        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_continueSpawning)
            if ((_obstacleLayer & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
            {
                collision.gameObject.SetActive(false);
                StartCoroutine(CreateAnObstacle(_generateDelay));
            }
        if ((_endLocationLayer & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
            _playerMovement.BlockMovement();
    }

    private void Start()
    {
        Invoke("SpawnEndLocation", _levelDuration);
    }


    private void SpawnEndLocation()
    {
        _continueSpawning = false;
        Instantiate(_endLocation, new Vector2(_player.position.x + _offset * 5, 0), new Quaternion());
    }

}

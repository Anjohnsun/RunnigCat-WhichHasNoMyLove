using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _roadTilePrefab;
    [SerializeField] private Transform _endOfTile;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(_roadTilePrefab, _endOfTile.position, new Quaternion());
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTag : MonoBehaviour
{
    [SerializeField] private ObstacleTypes _type;
    public ObstacleTypes Type => _type;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private int obstacleValue;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerTower playerTower))
        {
            playerTower.RemoveHuman(obstacleValue);
        }
    }
}

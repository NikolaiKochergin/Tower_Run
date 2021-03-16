using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class LevelCreator : MonoBehaviour
{
    [SerializeField] private PathCreator _pathCreator;
    [SerializeField] private Tower _towerTemplate;
    [SerializeField] private int _humanTowerCount;
    [SerializeField] private float _jumpEnhancerDistance;
    [SerializeField] private Obstacle _obstacleTemplate;
    [SerializeField] private float _obstacleDistance;

    private void Start()
    {
        GenerateLevel();
    }

    private void GenerateLevel()
    {
        float roadLength = _pathCreator.path.length;
        float distanceBetwenTower = roadLength / _humanTowerCount;

        float distanceTravelled = 0;
        Vector3 towerSpawnPoint;
        Vector3 jumpEnhancerPoint;
        Vector3 obstacleSpawnPoint;

        for (int i = 0; i < _humanTowerCount; i++)
        {
            distanceTravelled += distanceBetwenTower;
            towerSpawnPoint = _pathCreator.path.GetPointAtDistance(distanceTravelled, EndOfPathInstruction.Stop);
            jumpEnhancerPoint = _pathCreator.path.GetPointAtDistance(distanceTravelled - _jumpEnhancerDistance, EndOfPathInstruction.Stop);
            obstacleSpawnPoint = _pathCreator.path.GetPointAtDistance(distanceTravelled + _obstacleDistance, EndOfPathInstruction.Stop);

            Tower tower = Instantiate(_towerTemplate, towerSpawnPoint, Quaternion.identity);
            tower.GetComponentInChildren<JumpEnhancer>().transform.position = jumpEnhancerPoint;

            Instantiate(_obstacleTemplate, obstacleSpawnPoint, Quaternion.identity);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private Vector2Int _humanInTowerRange;
    [SerializeField] private Human[] _humansTemplates;
    [SerializeField] private float _bounceForce;
    [SerializeField] private float _bounceRadius;

    private List<Human> _humansInTower;

    public float HumansInTowerCount => _humansInTower.Count;

    private void Start()
    {
        _humansInTower = new List<Human>();
        int humanInTowerCount = Random.Range(_humanInTowerRange.x, _humanInTowerRange.y);
        SpawnHumans(humanInTowerCount);
    }

    private void SpawnHumans(int humanCount)
    {
        Vector3 spawnPoint = transform.position;

        for (int i = 0; i < humanCount; i++)
        {
            Human spawnedHuman = _humansTemplates[Random.Range(0, _humansTemplates.Length)];

            _humansInTower.Add(Instantiate(spawnedHuman, spawnPoint, Quaternion.identity, transform));

            _humansInTower[i].transform.localPosition = new Vector3(0, _humansInTower[i].transform.localPosition.y, 0);

            spawnPoint = _humansInTower[i].FixationPoint.position;

            switch (Random.Range(0, 5))
            {
                case 0:
                    _humansInTower[i].Texting();
                    break;
                case 1:
                    _humansInTower[i].Waving();
                    break;
                default:
                    break;
            }
        }
    }

    public List<Human> CollectHuman(Transform distanceChecker, float fixationMaxDistance)
    {
        for (int i = 0; i < _humansInTower.Count; i++)
        {
            float distanceBetweenPoints = CheckDistanceY(distanceChecker, _humansInTower[i].FixationPoint.transform);
            if (distanceBetweenPoints < fixationMaxDistance)
            {
                List<Human> collectedHumans = _humansInTower.GetRange(0, i + 1);
                _humansInTower.RemoveRange(0, i + 1);
                return collectedHumans;
            }
        }
        return null;
    }

    private float CheckDistanceY(Transform distanceChecker, Transform humanFixationPoint)
    {
        Vector3 distanceCheckerY = new Vector3(0, distanceChecker.position.y, 0);
        Vector3 humanFixationPointY = new Vector3(0, humanFixationPoint.position.y, 0);
        return Vector3.Distance(distanceCheckerY, humanFixationPointY);
    }

    public void Break()
    {
        Human[] humans = GetComponentsInChildren<Human>();

        foreach (var human in humans)
        {
            if (!human.TryGetComponent(out Rigidbody rigidbody))
            {
                human.gameObject.AddComponent<Rigidbody>();
            }

            human.Bounce(_bounceForce, transform.position, _bounceRadius);
        }
    }
}

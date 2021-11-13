using System.Collections.Generic;
using UnityEngine;

public class EntityPlacer : MonoBehaviour
{
    [SerializeField]
    private Entity _pig;

    [SerializeField]
    private Entity[] _enemies;

    [SerializeField]
    private Node[] _pigSpawnPoints;

    [SerializeField]
    private Node[] _enemiesSpawnPoints;

    private void Start()
    {
        if (_pig == null ||
            _enemies == null ||
            _pigSpawnPoints == null ||
            _enemiesSpawnPoints == null)
            return;

        GameObject pig = Instantiate(_pig.gameObject);
        Node pigNode = _pigSpawnPoints[Random.Range(0, _pigSpawnPoints.Length)];
        Entity pigEntity = pig.GetComponent<Entity>();

        pigEntity.Initialize();

        pigEntity.Pathfinder.SetStartingNode(pigNode);

        List<Node> openPositions = new List<Node>();

        foreach (var point in _enemiesSpawnPoints)
        {
            openPositions.Add(point);
        }

        foreach (var enemy in _enemies)
        {
            GameObject e = Instantiate(enemy.gameObject);
            int r = Random.Range(0, openPositions.Count);
            Node rNode = openPositions[r];
            Entity enemyEntity = e.GetComponent<Entity>();
            enemyEntity.Initialize();

            enemyEntity.Pathfinder.SetStartingNode(rNode);

            openPositions.RemoveAt(r);
        }
    }
}

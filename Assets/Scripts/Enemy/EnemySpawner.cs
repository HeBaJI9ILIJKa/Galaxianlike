using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnArea;

    [SerializeField] private Playership _player;

    [SerializeField] private Enemy[] _enemyPrefabs;

    [SerializeField] private float _spawnCooldown = 1.5f;

    [SerializeField] private ProjectilePool _enemyProjectilePool;

    private Coroutine _spawnRandomEnemy;

    private int _chancesSum;
    private List<int> _allChances = new List<int>();

    private int _currentEnemyAmount = 0;

    private void Start()
    {
        StartSpawning();
    }

    public void StartSpawning()
    {
        PrepareSpawnChances(_enemyPrefabs);

        _spawnRandomEnemy = StartCoroutine(SpawnRandomEnemyInArea());
    }

    public void StopSpawning()
    {
        StopCoroutine(_spawnRandomEnemy);
    }

    private void PrepareSpawnChances(Enemy[] enemyPrefabs)
    {
        _chancesSum = 0;
        _allChances.Clear();

        foreach (var enemyPrefab in enemyPrefabs)
        {
            _chancesSum += enemyPrefab.SpawnChance;
            _allChances.Add(enemyPrefab.SpawnChance);
        }
    }

    private IEnumerator SpawnRandomEnemyInArea()
    {
        while (Game.GameRunning)
        {
            if (_currentEnemyAmount < ScoreController.TotalEnemy - ScoreController.Score)
                SpawnEnemy(GetRandomEnemy(_enemyPrefabs), GetRandomSpawnPositionInArea(_spawnArea));
            yield return new WaitForSeconds(_spawnCooldown);
        }
    }

    private Enemy GetRandomEnemy(Enemy[] enemyPrefabs)
    {
        int value = Random.Range(0, _chancesSum);
        int sum = 0;

        for (int i = 0; i < _allChances.Count; i++)
        {
            sum += _allChances[i];
            if (value < sum)
                return enemyPrefabs[i];
        }

        return enemyPrefabs[enemyPrefabs.Length - 1];
    }

    private Vector3 GetRandomSpawnPositionInArea(Transform area)
    {
        Vector3 position = new Vector3();
        position.x = Random.Range(0, area.localScale.x) + area.position.x - area.localScale.x / 2;
        position.z = Random.Range(0, area.localScale.z) + area.position.z - area.localScale.z / 2;
        position.y = 0;

        Collider[] colliders = Physics.OverlapSphere(position, 1f);

        if (colliders.Length > 1)
            return GetRandomSpawnPositionInArea(_spawnArea);
        else
            return position;
    }

    private void SpawnEnemy(Enemy enemyPrefab, Vector3 position)
    {
        var enemy = Instantiate(enemyPrefab);
        enemy.SetPosition(position);
        enemy.SetTarget(_player);
        enemy.SetProjectilePool(_enemyProjectilePool);
        enemy.StartShooting();
        enemy.OnDestroy += EnemyDetroyed;
        _currentEnemyAmount++;
    }

    private void EnemyDetroyed(Enemy enemy)
    {
        _currentEnemyAmount--;
        enemy.OnDestroy -= EnemyDetroyed;
    }
}

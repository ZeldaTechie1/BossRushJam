using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveController : MonoBehaviour
{

    [SerializeField]private List<GameObject> _enemiesToSpawn;//change this to have a more Generic Enemy type
    [SerializeField]private List<Transform> _spawnPoints;
    [SerializeField]private int _maxEnemiesThisWave;
    [SerializeField]private int _firstWaveEnemyCount;

    private int _currentEnemiesAlive;
    private float _enemySpawnInterval;
    private bool _stopSpawning;

    private void Start()
    {
        _currentEnemiesAlive = 0;
        _maxEnemiesThisWave = _firstWaveEnemyCount;
    }

    private void StartWave()
    {

    }

    private void SpawnEnemy()
    {
        if(_enemiesToSpawn == null || _spawnPoints == null)
        {
            throw new System.Exception("EnemyWaveController is not properly setup and cannot spawn enemies!");
        }
        if(_stopSpawning)
        {
            return;
        }
        int randEnemy = Random.Range(0, _enemiesToSpawn.Count);
        int randSpawnPoint = Random.Range(0,_spawnPoints.Count);
        GameObject spawnedEnemy = Instantiate(_enemiesToSpawn[randEnemy], _spawnPoints[randSpawnPoint].position, Quaternion.identity,this.transform);
    }
}

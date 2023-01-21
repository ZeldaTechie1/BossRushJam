using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveController : MonoBehaviour
{

    [SerializeField]private List<GameObject> _enemiesToSpawn;//change this to have a more Generic Enemy type
    [SerializeField]private List<Transform> _spawnPoints;
    [SerializeField]private List<int> _enemyCountPerWave;
    [SerializeField]private float _enemySpawnInterval;
    [SerializeField]private float _maxIntervalDeviation;

    private int _currentEnemiesAlive;
    private int _currentWave;
    private bool _stopSpawning;

    private void StartWave()
    {
        Sequence waveSequence = DOTween.Sequence();
        for(int count = 0; count < _enemyCountPerWave[_currentWave]; count++)
        {
            float randomDeviation = Random.Range(0, _maxIntervalDeviation);
            waveSequence.AppendCallback(SpawnEnemy).AppendInterval(_enemySpawnInterval + randomDeviation);
        }
    }
    
    private void FinishedWave()
    {
        _currentWave++;
    }

    private void SpawnEnemy()
    {
        if(_enemiesToSpawn == null || _spawnPoints == null)
        {
            throw new System.Exception("EnemyWaveController is not properly setup and cannot spawn enemies!");
        }
        if(_stopSpawning)
            return;

        int randEnemy = Random.Range(0, _enemiesToSpawn.Count);
        int randSpawnPoint = Random.Range(0,_spawnPoints.Count);
        GameObject spawnedEnemy = Instantiate(_enemiesToSpawn[randEnemy], _spawnPoints[randSpawnPoint].position, Quaternion.identity,this.transform);
    }

    private void EnemyDied()
    {
        _currentEnemiesAlive--;
        if (_currentEnemiesAlive == 0)
        {
            FinishedWave();
        }
    }
}

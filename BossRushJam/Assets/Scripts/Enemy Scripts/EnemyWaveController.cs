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
    [SerializeField]private GameObject _player;//change this to take a Player object instead of Gameobject

    [SerializeField] private List<GameObject> enemies;
    private int _currentEnemiesAlive;
    private int _currentWave;
    private bool _stopSpawning;
    private bool _waveActive;

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            HurtRandomEnemy();
        }
    }

    public void StartWave()
    {
        _waveActive = true;
        Sequence waveSequence = DOTween.Sequence();
        for(int count = 0; count < _enemyCountPerWave[_currentWave]; count++)
        {
            float randomDeviation = Random.Range(0, _maxIntervalDeviation);
            waveSequence.AppendCallback(SpawnEnemy).AppendInterval(_enemySpawnInterval + randomDeviation);
        }
        waveSequence.OnComplete(() => _stopSpawning = true);
    }
    
    private void FinishedWave()
    {
        Debug.Log("Wave finished!");
        _currentWave++;
        _stopSpawning = false;
        _waveActive = false;
    }

    private void SpawnEnemy()
    {
        if(_enemiesToSpawn == null || _spawnPoints == null)
        {
            throw new System.Exception("EnemyWaveController is not properly setup and cannot spawn enemies!");
        }
        if(_stopSpawning)
            return;
        if (_player == null)
            return;

        int randEnemy = Random.Range(0, _enemiesToSpawn.Count);
        int randSpawnPoint = Random.Range(0,_spawnPoints.Count);
        GameObject spawnedEnemy = Instantiate(_enemiesToSpawn[randEnemy], _spawnPoints[randSpawnPoint].position, Quaternion.identity,this.transform);
        if(_player == null)
        {
            throw new System.Exception("Player is null! Waduhek");
        }
        enemies.Add(spawnedEnemy);//testing for emotional damage
        spawnedEnemy.GetComponent<Enemy>().SetPlayer(_player);
        _currentEnemiesAlive++;
    }

    public void EnemyDied(Component sender, object data)
    {
        if (!_waveActive)
            return;
        enemies.RemoveAt(enemies.IndexOf(sender.gameObject));
        Destroy(sender.gameObject);
        Debug.Log("Enemy Died!");
        _currentEnemiesAlive--;
        if (_currentEnemiesAlive == 0)
        {
            FinishedWave();
        }
    }

    //TESTING PURPOSES PLEASE DO NOT USE, K THX!
    public void HurtRandomEnemy()
    {
        Debug.Log("hurting random enemy");
        int randEnemy = Random.Range(0,enemies.Count);
        enemies[randEnemy].GetComponent<Health>()?.TakeDamage(5);
    }
}

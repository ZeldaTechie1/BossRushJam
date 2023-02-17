using Core;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveController : Singleton<EnemyWaveController>
{
    [SerializeField]private List<Transform> _spawnPoints;
    [SerializeField]private List<int> _enemyCountPerPhase;
    [SerializeField]private float _enemySpawnInterval;
    [SerializeField]private float _maxIntervalDeviation;
    [SerializeField]private GameObject _droppableItemPrefab;
    [SerializeField]private List<ItemObject> _itemsToDrop;
    [SerializeField]protected GameAudioEventManager _gameAudioEventManager;

    private GameObject _player;
    private List<Enemy> _enemiesToSpawn;
    private List<Enemy> _currentEnemiesAlive = new List<Enemy>();
    private int _currentPhase = -1;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        Boss.BossSpawned += UpdateEnemyList;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Home))
        {
            StartNextPhase();
        }
    }

    public void UpdateEnemyList(Boss boss)
    {
        _enemiesToSpawn = boss.MinionsToSpawn;
    }

    private void StartPhase()
    {
        Sequence waveSequence = DOTween.Sequence();
        for (int count = 0; count < _enemyCountPerPhase[_currentPhase]; count++)
        {
            float randomDeviation = Random.Range(0, _maxIntervalDeviation);
            waveSequence.AppendCallback(SpawnEnemy).AppendInterval(_enemySpawnInterval + randomDeviation);
        }
    }
    
    public void StartNextPhase()
    {
        Debug.Log("Next Phase!");
        _currentPhase++;
        if (_currentPhase > 2)
        {
            _currentPhase = 0;
        }
        StartPhase();
    }

    private void SpawnEnemy()
    {
        if(_enemiesToSpawn == null || _spawnPoints == null)
        {
            Debug.Log(_enemiesToSpawn);
            Debug.Log(_spawnPoints);
            throw new System.Exception("EnemyWaveController is not properly setup and cannot spawn enemies!");
        }
        if (_player == null)
            return;
        if (_currentEnemiesAlive.Count >= _enemyCountPerPhase[_currentPhase])
            return;
        int randEnemy = Random.Range(0, _enemiesToSpawn.Count);
        int randSpawnPoint = Random.Range(0,_spawnPoints.Count);
        Enemy spawnedEnemy = Instantiate(_enemiesToSpawn[randEnemy], _spawnPoints[randSpawnPoint].position, Quaternion.identity,this.transform);
        if(_player == null)
        {
            throw new System.Exception("Player is null! Waduhek");
        }
        _currentEnemiesAlive.Add(spawnedEnemy);//testing for emotional damage
        spawnedEnemy.GetComponent<Enemy>().SetPlayer(_player);
    }

    public void EnemyDied(Component sender, object data)
    {
        _gameAudioEventManager.GetComponent<GameAudioEventManager>().PlayEnemyDie();

        GameObject a = Instantiate(_droppableItemPrefab, sender.transform.position, Quaternion.identity);
        a.GetComponentInChildren<DroppableItem>().Init(_itemsToDrop[Random.Range(0, _itemsToDrop.Count)]);
        _currentEnemiesAlive.RemoveAt(_currentEnemiesAlive.IndexOf(sender.gameObject.GetComponent<Enemy>()));
        Destroy(sender.gameObject);
        if(_currentEnemiesAlive.Count < _enemyCountPerPhase[_currentPhase])
        {
            float randomDeviation = Random.Range(0, _maxIntervalDeviation);
            DOTween.Sequence().InsertCallback(_enemySpawnInterval + randomDeviation, SpawnEnemy);
        }
    }

    //TESTING PURPOSES PLEASE DO NOT USE, K THX!
    /*public void HurtRandomEnemy()
    {
        Debug.Log("hurting random enemy");
        int randEnemy = Random.Range(0,_currentEnemiesAlive.Count);
        _currentEnemiesAlive[randEnemy].GetComponent<Health>()?.AffectHealth(null, data: 5f);
    }*/ 
}

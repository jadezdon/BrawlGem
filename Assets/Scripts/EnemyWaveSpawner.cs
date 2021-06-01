using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveSpawner : MonoBehaviour
{
    public enum SpawnState { SPAWING, WAITING, COUNTING, COMPLETED }

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemyPrefab;
        public int count;
        public float rate;
    }

    public Wave[] waves;
    public float waveDelay = 5f;
    public SpawnState state = SpawnState.COUNTING;
    public Transform[] spawnPoints;

    float waveCountdown;
    float searchCountdown = 1f;
    int nextWaveIndex = 0;

    void Start()
    {
        if (spawnPoints.Length <= 0) Debug.LogError("No spawn points referenced");

        waveCountdown = waveDelay;
    }

    void Update()
    {
        if (state == SpawnState.COMPLETED) return;

        if (state == SpawnState.WAITING)
        {
            if (EnemyIsAlive()) return;

            // new wave
            WaveCompleted();
        }

        if (waveCountdown <= 0)
        {
            if (state != SpawnState.SPAWING)
            {
                // start spawning wave
                StartCoroutine(SpawnWave(waves[nextWaveIndex]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawing wave " + _wave.name);

        state = SpawnState.SPAWING;

        // Spawn
        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemyPrefab);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = SpawnState.WAITING;
        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        Transform _spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy, _spawnPoint.position, _spawnPoint.rotation);
    }

    void WaveCompleted()
    {
        Debug.Log("Wave completed");

        state = SpawnState.COUNTING;
        waveCountdown = waveDelay;

        if (nextWaveIndex + 1 >= waves.Length)
        {
            Debug.Log("ALL WAVES COMPLETED!");
            state = SpawnState.COMPLETED;
            LevelManager.Instance.CompleteLevel();
            return;
        }

        nextWaveIndex++;
    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown > 0f) return true;

        searchCountdown = 1f;
        return (GameObject.FindGameObjectsWithTag("Enemy").Length != 0);
    }
}

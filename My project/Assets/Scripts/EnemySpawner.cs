using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public Enemy spawnedEnemy;

    [SerializeField] private int minimumKillsToIncreaseSpawnCount = 3;
    public int totalKill = 0;
    private int totalKillWave = 0;

    [SerializeField] private float spawnInterval = 3f;

    [Header("Spawned Enemies Counter")]
    public int spawnCount = 0;
    public int defaultSpawnCount = 1;
    public int spawnCountMultiplier = 1;
    public int multiplierIncreaseCount = 1;

    public CombatManager combatManager;

    public bool isSpawning = false;
    private int currentWaveEnemyCount = 0;

    public void StartNextWave()
    {
        if (combatManager.waveNumber >= spawnedEnemy.level)
        {
            StartCoroutine(SpawnEnemies());
        }
    }

    private IEnumerator SpawnEnemies()
    {
        isSpawning = true;
        spawnCount = (spawnedEnemy.level <= combatManager.waveNumber) ? defaultSpawnCount : 0;
        currentWaveEnemyCount = spawnCount;

        while (spawnCount > 0)
        {
            Enemy enemyInstance = Instantiate(spawnedEnemy, transform);
            enemyInstance.enemySpawner = this;
            spawnCount--;
            yield return new WaitForSeconds(spawnInterval);
        }

        isSpawning = false;
    }

    public void OnEnemyKilled()
    {
        totalKill++;
        totalKillWave++;
        currentWaveEnemyCount--;

        if (totalKillWave >= minimumKillsToIncreaseSpawnCount)
        {
            defaultSpawnCount += spawnCountMultiplier;
            totalKillWave = 0;
        }

        combatManager?.OnEnemyKilled(currentWaveEnemyCount);
    }
}
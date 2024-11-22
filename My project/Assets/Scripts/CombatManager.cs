using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public EnemySpawner[] enemySpawners;
    public float timer = 0;
    [SerializeField] private float waveInterval = 5f;
    public int waveNumber = 0;
    public int totalEnemies = 0;
    private bool isWaitingForEnemiesElimination = false;

    private void Update()
    {
        if (isWaitingForEnemiesElimination)
        {
            return;
        }

        if (timer >= waveInterval)
        {
            waveNumber++;
            timer = 0;
            isWaitingForEnemiesElimination = true;
            NotifySpawners();
        }
        else if (totalEnemies <= 0 || waveNumber == 0)
        {
            timer += Time.deltaTime;
        }
    }

    private void NotifySpawners()
    {
        foreach (var spawner in enemySpawners)
        {
            spawner.StartNextWave();
        }
    }

    public void OnEnemyKilled(int remainingEnemiesInWave)
    {
        totalEnemies--;

        if (isWaitingForEnemiesElimination && remainingEnemiesInWave <= 0)
        {
            isWaitingForEnemiesElimination = false;
        }
    }
}
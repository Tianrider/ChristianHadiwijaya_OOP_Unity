using System.Collections;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public EnemySpawner[] enemySpawners;

    public float timer = 0;
    [SerializeField] private float waveInterval = 5f;

    public int waveNumber = 1;

    public int totalEnemies = 0;
    public int points = 0;

    private void OnEnable()
    {
        foreach (EnemySpawner spawner in enemySpawners)
        {
            spawner.combatManager = this;
        }
    }

    private void FixedUpdate()
    {
        if (totalEnemies == 0)
            timer += Time.deltaTime;

        if (timer >= waveInterval)
        {
            foreach (EnemySpawner spawner in enemySpawners)
            {
                if (spawner.spawnedEnemy.GetLevel() <= waveNumber && !spawner.isSpawning)
                {
                    spawner.ResetSpawnCount();

                    totalEnemies += spawner.spawnCount;

                    spawner.SpawnEnemy();
                }
            }

            waveNumber++;
            timer = 0;
        }
    }

    public void IncreaseKill()
    {
        totalEnemies--;
    }
}

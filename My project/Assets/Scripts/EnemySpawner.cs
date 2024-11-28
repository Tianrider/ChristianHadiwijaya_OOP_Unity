using System.Collections;
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

    private void Start()
    {
        spawnCount = defaultSpawnCount;
    }

    public void SpawnEnemy()
    {
        StartCoroutine(IESpawnEnemy());
    }

    IEnumerator IESpawnEnemy()
    {
        isSpawning = true;

        while (spawnCount > 0)
        {
            Enemy s = Instantiate(spawnedEnemy);

            s.transform.parent = gameObject.transform;

            s.enemyKilledEvent.AddListener(KillEnemy);
            s.enemyKilledEvent.AddListener(combatManager.IncreaseKill);

            spawnCount--;

            yield return new WaitForSeconds(spawnInterval);
        }

        isSpawning = false;
    }

    public void ResetSpawnCount()
    {
        if (totalKillWave >= minimumKillsToIncreaseSpawnCount)
        {
            spawnCountMultiplier += multiplierIncreaseCount;
            minimumKillsToIncreaseSpawnCount *= spawnCountMultiplier;
            totalKillWave = 0;
        }

        spawnCount = defaultSpawnCount * spawnCountMultiplier;
    }

    private void KillEnemy()
    {
        totalKill++;
        totalKillWave++;
        combatManager.points += spawnedEnemy.GetLevel();
    }
}

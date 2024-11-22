using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int level = 1;
    public EnemySpawner enemySpawner;

    void Awake()
    {
        // Disable gravity
        GetComponent<Rigidbody2D>().gravityScale = 0f;
    }

    // Pastikan Enemy menghadap ke arah Player
    public void FacePlayer(Transform playerTransform)
    {
        if (playerTransform.position.x < transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 180f, 0);
        }
        else
        {
            transform.eulerAngles = Vector3.zero;
        }
    }

    public void OnDestroy()
    {
        enemySpawner.OnEnemyKilled();
    }
}
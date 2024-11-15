using UnityEngine;

public class EnemyTargeting : Enemy
{
    public float speed = 2f;
    private Transform playerTransform;

    private float screenHeight;
    private float screenWidth;

    void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
        // Dapatkan transform dari Player
        screenHeight = Camera.main.orthographicSize * 2f;
        screenWidth = Camera.main.orthographicSize * Camera.main.aspect * 2f;

        float randomX = Random.Range(-screenWidth / 2f, screenWidth / 2f);
        float randomY = Random.Range(-screenHeight / 2f, screenHeight / 2f);

        // Set posisi enemy
        transform.position = new Vector3(randomX, randomY, transform.position.z);
    }

    void Update()
    {
        // Gerakkan enemy ke arah Player
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);

        // Pastikan Enemy menghadap ke arah Player
        FacePlayer(playerTransform);

        // Jika Enemy menyentuh Player, hancurkan Enemy
        if (Vector3.Distance(transform.position, playerTransform.position) < 0.5f)
        {
            Destroy(gameObject);
        }
    }
}
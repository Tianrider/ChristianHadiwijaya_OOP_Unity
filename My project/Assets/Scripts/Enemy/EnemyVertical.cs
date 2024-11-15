using UnityEngine;

public class EnemyForward : Enemy
{
    public float speed = 2f;
    private float screenHeight;
    private float screenWidth;

    void Start()
    {
        // Dapatkan tinggi layar
        screenHeight = Camera.main.orthographicSize * 2f;
        screenWidth = Camera.main.orthographicSize * Camera.main.aspect * 2f;

        float randomX = Random.Range(-screenWidth / 2f, screenWidth / 2f);

        // Set posisi enemy
        transform.position = new Vector3(randomX, screenHeight / 2f, transform.position.z);
    }

    void Update()
    {
        // Gerakkan enemy ke bawah
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        // Jika enemy keluar dari layar, hancurkan
        if (transform.position.y < -screenHeight / 2f)
        {
            Destroy(gameObject);
        }
    }
}
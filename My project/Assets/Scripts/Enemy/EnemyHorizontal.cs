using UnityEngine;

public class EnemyHorizontal : Enemy
{
    public float speed = 2f;
    private bool movingRight = true;
    float screenWidth;
    void Start()
    {
        // Dapatkan lebar layar
        float screenHeight = Camera.main.orthographicSize * 2f;
        screenWidth = Camera.main.orthographicSize * Camera.main.aspect * 2f;

        // give offset to enemy so it's not really on the edge of the screen
        float randomY = Random.Range(-screenHeight / 2f + 1, screenHeight / 2f - 1);

        // Set posisi enemy
        transform.position = new Vector3(screenWidth / 2f, randomY, transform.position.z);
    }

    void Update()
    {
        // Gerakkan enemy horizontal
        float movement = movingRight ? speed : -speed;
        transform.Translate(Vector3.right * movement * Time.deltaTime);

        // Jika enemy keluar dari layar, ubah arah geraknya
        if (movingRight && transform.position.x > screenWidth / 2f)
        {
            movingRight = false;
        }
        else if (!movingRight && transform.position.x < -screenWidth / 2f)
        {
            movingRight = true;
        }
    }
}
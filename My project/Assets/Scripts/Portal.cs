using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float rotateSpeed;

    Vector2 newPosition;

    // Start is called before the first frame update
    void Start()
    {
        changePosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, newPosition) < 0.5)
        {
            changePosition();
        }

        transform.position = Vector2.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
        transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered portal");
            GameManager.Instance.LevelManager.LoadScene("Main");
        }
    }

    void changePosition()
    {
        newPosition = new Vector2(Random.Range(-8, 8), Random.Range(-4, -5));
    }
}

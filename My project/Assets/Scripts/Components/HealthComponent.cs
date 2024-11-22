using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int health;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    public int getHealth()
    {
        return health;
    }

    public void Subtract(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}

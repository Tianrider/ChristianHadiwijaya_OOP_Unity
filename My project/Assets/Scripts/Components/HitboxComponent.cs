using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HitboxComponent : MonoBehaviour
{

    [SerializeField] private HealthComponent health;
    private InvincibilityComponent invincibility;

    void Awake()
    {
        Collider2D collider = GetComponent<Collider2D>();
        if (collider == null)
        {
            Debug.LogError("HitboxComponent requires a Collider2D component.");
            return;
        }

        collider.isTrigger = true;

        invincibility = GetComponent<InvincibilityComponent>();
    }

    public void Damage(int damage)
    {
        if (health != null && (invincibility == null || !invincibility.isInvincible))
        {
            health.subtract(damage);
        }
    }

    public void Damage(Bullet bullet)
    {
        if (health != null && (invincibility == null || !invincibility.isInvincible))
        {
            health.subtract(bullet.damage);
        }
    }

}

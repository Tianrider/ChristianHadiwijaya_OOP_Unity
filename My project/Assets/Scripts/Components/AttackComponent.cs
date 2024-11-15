using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AttackComponent : MonoBehaviour
{
    [SerializeField] public int damage = 10;
    [SerializeField] private Bullet bullet;

    // Start is called before the first frame update
    private void Awake()
    {
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.isTrigger = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            return;
        }

        HitboxComponent hitbox = other.GetComponent<HitboxComponent>();
        if (hitbox != null)
        {
            InvincibilityComponent invincibilityComponent = other.GetComponent<InvincibilityComponent>();
            if (invincibilityComponent != null)
            {
                invincibilityComponent.StartInvincibility();
            }

            Bullet bullet = GetComponent<Bullet>();
            if (bullet != null)
            {
                hitbox.Damage(bullet);
            }
            else
            {
                hitbox.Damage(damage);
            }
        }
    }
}

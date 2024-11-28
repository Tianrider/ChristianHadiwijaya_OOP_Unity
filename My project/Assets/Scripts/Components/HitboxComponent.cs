using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HitboxComponent : MonoBehaviour
{
    [SerializeField]
    HealthComponent health;

    Collider2D area;

    private InvincibilityComponent invincibilityComponent;


    void Start()
    {
        area = GetComponent<Collider2D>();
        invincibilityComponent = GetComponent<InvincibilityComponent>();
    }

    public void Damage(Bullet bullet)
    {
        Debug.Log("HitboxComponent.Damage() Bullet");
        if (invincibilityComponent != null && invincibilityComponent.isInvincible)
        {
            Debug.Log("HitboxComponent.Damage() is invincible");
            return;
        }

        if (health != null)
            health.Subtract(bullet.damage);
    }

    public void Damage(int damage)
    {
        Debug.Log("HitboxComponent.Damage() int");
        if (invincibilityComponent != null && invincibilityComponent.isInvincible) return;

        if (health != null)
            health.Subtract(damage);
    }
}

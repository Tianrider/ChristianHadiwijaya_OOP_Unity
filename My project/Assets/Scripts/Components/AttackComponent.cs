using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AttackComponent : MonoBehaviour
{
    public Bullet bullet;
    public int damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(gameObject.tag)) return;

        if (other.GetComponent<HitboxComponent>() != null)
        {
            Debug.Log("HitboxComponent found");
            HitboxComponent hitbox = other.GetComponent<HitboxComponent>();

            if (bullet != null)
            {
                hitbox.Damage(bullet.damage);
            }

            hitbox.Damage(damage);
        }

        if (other.GetComponent<InvincibilityComponent>() != null)
        {
            other.GetComponent<InvincibilityComponent>().TriggerInvincibility();
        }
    }
}

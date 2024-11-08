using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] Weapon weaponHolder;
    Weapon weapon;

    private void Awake()
    {
        if (weaponHolder != null)
        {
            weapon = Instantiate(weaponHolder, transform.position, transform.rotation);
            weapon.gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        Debug.Log("WeaponPickup started");
        if (weapon != null)
        {
            TurnVisual(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Triggered");
        if (other.CompareTag("Player"))
        {
            if (weapon != null)
            {
                Debug.Log("Weapon found");
                Weapon currentWeapon = other.GetComponentInChildren<Weapon>();

                if (currentWeapon != null)
                {
                    Debug.Log("Weapon found in player");
                    currentWeapon.gameObject.SetActive(false);
                }

                Weapon newWeapon = Instantiate(weapon, other.transform);
                newWeapon.transform.localPosition = new Vector3(0, 0, 1);
                newWeapon.gameObject.SetActive(true);

                TurnVisual(true, newWeapon);
            }
        }
    }

    private void TurnVisual(bool on)
    {
        if (weapon != null)
        {
            weapon.gameObject.SetActive(on);
        }
    }

    private void TurnVisual(bool on, Weapon weapon)
    {
        if (weapon != null)
        {
            weapon.gameObject.SetActive(on);
        }
    }
}
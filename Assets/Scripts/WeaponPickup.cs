using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] Weapon.WeaponType weaponType;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Weapon>() != null)
        {
            other.GetComponent<Weapon>().ChangeWeapon(weaponType);
        }

        Destroy(gameObject);
    }
}

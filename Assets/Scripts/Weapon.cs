using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    public enum WeaponType
    {
        pistol,
        smg,
        sniper
    }

    [Header("Basic Weapon Stats")]
    [SerializeField] private WeaponType weaponType;
    [SerializeField] private bool automatic; //boolean recording if the weapon is automatic
    [SerializeField] private float damage; //damage done
    [SerializeField] private float range; //range in unity unitys
    [SerializeField] private float fireRate; //bullets per second
    [SerializeField] private float deviation; //deviation of shot angle in degrees
    [SerializeField] private int magSize; //size of a magazine
    [SerializeField] private float reloadTime; //time it takes to reload
    [SerializeField] private int totalAmmo; //total ammo for weapon
    [SerializeField] private float critChance; //the percentage change that a crit will occur
    [SerializeField] private float critMult; //the multiplier for damage done on crit

    [Header("Weapon Fire Visuals")]
    [SerializeField] private Transform muzzle;
    [SerializeField] private GameObject bulletTrail;
    [SerializeField] private Animator muzzFlashAnimator;

    //tracked values
    private int ammoInMag;
    private bool cooldown = false;

    public void ChangeWeapon (WeaponType weaponType)
    {
        this.weaponType = weaponType;

        switch (weaponType)
        {
            case WeaponType.pistol:
                automatic = false;
                damage = 5f;
                range = 12f;
                fireRate = 2.5f;
                deviation = 10f;
                magSize = 12;
                totalAmmo = 60;
                critChance = 5;
                critMult = 1.5f;
                break;

            case WeaponType.smg:
                automatic = true;
                damage = 2f;
                range = 6f;
                fireRate = 10f;
                deviation = 60f;
                magSize = 30;
                totalAmmo = 150;
                critChance = 3;
                critMult = 1.5f;
                break;

            case WeaponType.sniper:
                automatic = false;
                damage = 20f;
                range = 25f;
                fireRate = 1f;
                deviation = 0f;
                magSize = 4;
                totalAmmo = 20;
                critChance = 5;
                critMult = 1.5f;
                break;
        }

        //mag changes
        ammoInMag = 0;
        Reload();
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeWeapon(WeaponType.pistol);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire()
    {
        /*
        //check mag
        if (ammoInMag > 0)
        {*/
            ammoInMag--; //remove 1 ammo

            //detection
            Vector3 direction = GetDirection();//gets direction for the raycast (accounting for deviation)
            var hit = Physics2D.Raycast(muzzle.position, direction, range); //raycast to detect hits
            Debug.DrawRay(muzzle.position, direction, Color.green, range);

            //bullet trail
            var trail = Instantiate(bulletTrail, muzzle.position, transform.rotation);
            var trailScript = trail.GetComponent<BulletTrail>();

            if (hit.collider != null)
            {
                trailScript.SetTargetPosition(hit.point);

                //hit damageable entity
                if (hit.collider.GetComponent<Health>() != null)
                {
                    //check for crit
                    if (Random.Range(0f, 100f) <= critChance)
                    {
                        hit.collider.GetComponent<Health>().TakeDamage(damage * critMult);
                    }
                    else
                    {
                        hit.collider.GetComponent<Health>().TakeDamage(damage);
                    }
                }
                else
                {
                    //some visual effect like particle system
                }
            }
            else
            {
                var endPos = muzzle.position + direction * range;
                trailScript.SetTargetPosition(endPos);
            }
        /*
        }
        else
        {
            //play click effect
        }
        */
    }

    public void Reload()
    {
        /*
        if (totalAmmo > 0)
        {

        }
        */
    }

    public int GetAmmoInMag()
    {
        return ammoInMag;
    }

    private Vector3 GetDirection()
    {
        float deviationAngle = Random.Range(-deviation/2, deviation/2); //determining the deviation of the bullet

        Vector3 direction3D = Quaternion.AngleAxis(deviationAngle, Vector3.forward) * transform.right; //applying transformation to base vector

        return direction3D;
    }
     
}

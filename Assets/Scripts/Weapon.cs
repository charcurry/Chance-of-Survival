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
    [SerializeField] private int totalAmmo; //total ammo for weapon
    [SerializeField] private float critChance; //the percentage change that a crit will occur
    [SerializeField] private float critMult; //the multiplier for damage done on crit

    [Header("Weapon Fire Visuals")]
    [SerializeField] private Transform muzzle;
    [SerializeField] private ParticleSystem muzzleFlash;

    //tracked values
    private int ammoInMag;
    private bool cooldown = false;

    public Weapon(WeaponType weaponType)
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
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire()
    {

    }

    public void Reload()
    {

    }

    public int GetAmmoInMag()
    {
        return ammoInMag;
    }
     
}

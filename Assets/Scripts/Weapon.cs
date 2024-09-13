using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEditor.ShaderGraph.Internal;
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

    [Header("Visuals")]
    [SerializeField] private Transform pistolMuzzle;
    [SerializeField] private Transform smgMuzzle;
    [SerializeField] private Transform sniperMuzzle;
    [SerializeField] private SpriteRenderer pistolSprite;
    [SerializeField] private SpriteRenderer smgSprite;
    [SerializeField] private SpriteRenderer sniperSprite;
    [SerializeField] private GameObject bulletTrail;
    //[SerializeField] private GameObject muzzleFlash;
    //[SerializeField] private SpriteRenderer muzzleFlashRenderer;
    //[SerializeField] private float flashtime = 0.05f;

    [Header("Debug")]
    [SerializeField] private WeaponType startingType;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI ammoText;

    //tracked values
    private Transform muzzle;
    private int ammoInMag;
    private bool cooldown = false;
    private bool buttonDown = false;

    public void ChangeWeapon (WeaponType weaponType)
    {
        Debug.Log("weapon change started");

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
                reloadTime = 2.5f;
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
                reloadTime = 4.5f;
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
                reloadTime= 4f;
                totalAmmo = 20;
                critChance = 5;
                critMult = 1.5f;
                break;
        }

        //switch sprite
        SwitchSprite();

        //switch muzzle
        SwitchMuzzle();

        //mag changes
        ammoInMag = 0;
        Reload();
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeWeapon(startingType);
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonDown && !cooldown)
        {
            Fire();
            StartCoroutine("FiringCooldown");

            if (!automatic)
            {
                buttonDown = false;
            }
        }

        ammoText.text = "Ammo: " + ammoInMag.ToString() + "/" + totalAmmo.ToString();
    }

    public void Attack(InputAction.CallbackContext input)
    {
        if (input.performed)
        {
            buttonDown = true;
        }
        if (input.canceled)
        {
            buttonDown = false;
        }
    }

    public void Fire()
    {
        
        //check mag
        if (ammoInMag > 0)
        {
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
        
        }
        else
        {
            Debug.Log("Gun empty");
            //play click effect
        }
        
    }

    public void Reload()
    {
        
        if (totalAmmo > 0)
        {
            StartCoroutine("LoadGun");
        }
        else
        {
            //play vine boom or something
        }
        
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

    private void SwitchSprite()
    {
        Debug.Log("Switch Sprite Started");

        switch (weaponType)
        {
            case WeaponType.pistol:
                Debug.Log("Switched to pistol");
                pistolSprite.enabled = true;
                smgSprite.enabled = false;
                sniperSprite.enabled = false;
                break;

            case WeaponType.smg:
                Debug.Log("Switched to smg");
                pistolSprite.enabled = false;
                smgSprite.enabled = true;
                sniperSprite.enabled = false;
                break;

            case WeaponType.sniper:
                Debug.Log("Switched to sniper");
                pistolSprite.enabled = false;
                smgSprite.enabled = false;
                sniperSprite.enabled = true;
                break;
        }
    }

    private void SwitchMuzzle()
    {

        switch (weaponType)
        {
            case WeaponType.pistol:
                muzzle = pistolMuzzle;
                //muzzleFlash.transform.position = pistolMuzzle.transform.position;
                break;

            case WeaponType.smg:
                muzzle = smgMuzzle;
                //muzzleFlash.transform.position = smgMuzzle.transform.position;
                break;

            case WeaponType.sniper:
                muzzle = sniperMuzzle;
                //muzzleFlash.transform.position = sniperMuzzle.transform.position;
                break;
        }
    }

    /*
    private IEnumerator MuzzleFlash()
    {
        float timer = 0f;
        muzzleFlashRenderer.color = new Color(255,255,255,255);

        while (muzzleFlashRenderer.color.a > 0)
        {
            timer += Time.deltaTime;
            muzzleFlashRenderer.color = new Color(255, 255, 255, Mathf.Lerp(255,0,timer/flashtime));
            yield return null;
        }
    }
    */

    //co-routine that loads/reload the gun
    private IEnumerator LoadGun()
    {
        Debug.Log("reload started");

        //implement load timer
        float timer = 0f;
        while (timer < reloadTime)
        {
            ammoText.text = "Reloading: " + ((int)(timer / reloadTime * 100)).ToString() + "%";
            timer += Time.deltaTime;
            yield return null;
        }

        //moving ammo around
        ammoInMag += totalAmmo;

        if (ammoInMag > magSize)
        {
            ammoInMag = magSize;
        }

        totalAmmo -= ammoInMag;

        if (totalAmmo < 0)
        {
            totalAmmo = 0;
        }

        Debug.Log("reload finished");
    }

    private IEnumerator FiringCooldown()
    {
        cooldown = true;
        float timer = 0f;

        while (timer < 1/fireRate)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        cooldown = false;
    }
     
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Basic Weapon Stats")]
    [SerializeField] private float damage;
    [SerializeField] private float range;
    [SerializeField] private float fireRate;
    [SerializeField] private float deviation;
    [SerializeField] private int magSize;
    [SerializeField] private int totalAmmo;
    [SerializeField] private float critChance = 5f;
    [SerializeField] private float critMult = 1.5f;

    //tracked values
    private int ammoInMag;

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

    public abstract void Reload();
    
}

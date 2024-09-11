using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health Variables")]
    [SerializeField] private float maxHealth;
    [SerializeField] private float damageRes;
    [SerializeField] private float evasionChance;

    //tracked variables
    private float currentHealth;

    public bool TakeDamage(float damage)
    {
        //see if evasion happens
        if (!(Random.Range(1f, 100f) <= evasionChance))
        {
            currentHealth -= damage / damageRes;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void heal(float healthAmount)
    {
        currentHealth += healthAmount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public float GetCurrentHealth()
    {
        return currentHealth; 
    }
}

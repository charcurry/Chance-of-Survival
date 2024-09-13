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

    private ScoreManager scoreManager;
    private EnemyStateMachine enemy;

    public void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        enemy = GetComponent<EnemyStateMachine>();
    }

    //Method that deals damage to entity
    public bool TakeDamage(float damage)
    {
        //see if evasion happens
        if (!(Random.Range(1f, 100f) <= evasionChance))
        {
            currentHealth -= damage / damageRes; //applies damage
            if (currentHealth <= 0) //checks if entity is dead
            {
                Destroy(gameObject);
                scoreManager.AddScore(500, enemy.enemyType);
                return true;
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    //method that heals damage on entity
    public void heal(float healthAmount)
    {
        currentHealth += healthAmount;

        //apply boundaries to health
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    //method that returns the max health of the entity
    public float GetMaxHealth()
    {
        return maxHealth;
    }

    //method that returns the current health of the entity
    public float GetCurrentHealth()
    {
        return currentHealth; 
    }
}

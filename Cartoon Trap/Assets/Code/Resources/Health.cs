using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health
{
    private int maxHealth;
    private int currentHealth;

    public int CurrentHealth { get => currentHealth;}
    public int MaxHealth { get => maxHealth;}

    public Health(int maxHealth, int currentHealth = -1)
    {
        this.maxHealth = maxHealth;

        if (currentHealth < 0)
        {
            this.currentHealth = maxHealth;
        }
        else
        {
            this.currentHealth = currentHealth;
        }
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }

    public void SetHealth(int health)
    {
        if (health > maxHealth)
        {
            health = maxHealth;

            
        }else if (health < 0)
        {
            health = 0;
        }

        currentHealth = health;
    }

    public void DecreaseHealth(int decreasedHealth)
    {
        SetHealth(currentHealth - decreasedHealth);
    }

    public void IncreaseHealth(int increasedHealth)
    {
        SetHealth(currentHealth + increasedHealth);
    }
}

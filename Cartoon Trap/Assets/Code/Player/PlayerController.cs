using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int maxHealth = 0;
    public int currentHealth = -1; //Variable temporal para poder probar la clase Health de manera comoda
    public Health playerHealth;

    private void Awake()
    {
        playerHealth = new Health(maxHealth, currentHealth);
    }

    private void FixedUpdate()
    {
        if (playerHealth.currentHealth == 0)
        {
            Die();
        }
    }

    private void Die()
    {
        throw new NotImplementedException();
    }
}

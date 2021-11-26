using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OnomatopeyaCollider), typeof(OnomaopeyaEffect))]
public class Onomatopeya : MonoBehaviour
{

    public float lifeTime;
    private float timeAlive;
    private bool active;

    private OnomaopeyaEffect effect;

    private int onoMaxUpgrades = 2;
    private int currentUpgrades;

    public bool Active { get => active; set => active = value; }

    private void Awake()
    {
        currentUpgrades = 0;
        timeAlive = 0;
        Active = false;
        effect = GetComponent<OnomaopeyaEffect>();
    }

    private void FixedUpdate()
    {
        timeAlive += Time.fixedDeltaTime;

        if (timeAlive >= lifeTime)
        {
            DestroyOnomatopeya();
        }
        else if(Active)
        {
            effect.ExecuteEffect();
        }
    }

    protected void DestroyOnomatopeya() //Metodo que podrian heredar los hijos en caso de que al destruirse las onomatopeyas hagan algo
    {
        Destroy(gameObject);
    }

    public void Upgrade()
    {
        if (currentUpgrades < onoMaxUpgrades)
        {
            transform.localScale *= 2f;
            ++currentUpgrades;
        }
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OnomatopeyaCollider), typeof(OnomaopeyaEffect))]
public class Onomatopeya : MonoBehaviour
{
    private bool active;

    protected OnomaopeyaEffect effect;

    protected int onoMaxUpgrades = 2;
    protected int currentUpgrades;

    public bool Active { get => active; set => active = value; }

    protected virtual void Awake()
    {
        currentUpgrades = 0;
        Active = false;
        effect = GetComponent<OnomaopeyaEffect>();
    }

    private void FixedUpdate()
    {
        if(Active)
        {
            effect.ExecuteEffect();
        }
    }

    protected void DestroyOnomatopeya() //Metodo que podrian heredar los hijos en caso de que al destruirse las onomatopeyas hagan algo
    {
        Destroy(gameObject);
    }

    public virtual void Upgrade()
    {
        if (currentUpgrades < onoMaxUpgrades)
        {
            transform.localScale *= 2f;
            ++currentUpgrades;
        }
    }

}

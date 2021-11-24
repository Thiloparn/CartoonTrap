using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Onomatopeya : MonoBehaviour
{

    public float lifeTime;
    private float timeAlive;
    private bool active;

    private OnomaopeyaEffect effect;

    public bool Active { get => active; set => active = value; }

    private void Awake()
    {
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

    private void DestroyOnomatopeya() //Metodo que podrian heredar los hijos en caso de que al destruirse las onomatopeyas hagan algo
    {
        Destroy(gameObject);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumEffect : OnomaopeyaEffect
{
    public override void InstantiateAreaEffect()
    {
        CreateAreaEffect();    }

    public void CreateAreaEffect()
    {
        Instantiate(ono.onoAreaEffect, transform.position, Quaternion.identity);
    }
}

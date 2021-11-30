using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumOnomatopeya : Onomatopeya
{
    public GameObject areaEffectCollider;

    protected override void Awake()
    {
        base.Awake();
    }

    public void CreateAreaEffect()
    {
        Instantiate(areaEffectCollider, transform.position, Quaternion.identity);
    }
}

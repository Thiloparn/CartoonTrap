using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchEffect : OnomaopeyaEffect
{

    public override void ExecuteEffect()
    {
        Instantiate(ono.onoAreaEffect, transform.position, Quaternion.identity);
        base.ExecuteEffect();
    }
}

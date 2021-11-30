using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumEffect : OnomaopeyaEffect
{
    public override void ExecuteEffect()
    {
        GetComponent<PumOnomatopeya>().CreateAreaEffect();
        base.ExecuteEffect();
    }
}

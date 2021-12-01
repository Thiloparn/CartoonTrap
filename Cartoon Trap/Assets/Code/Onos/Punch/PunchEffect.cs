using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchEffect : OnomaopeyaEffect
{
    public GameObject punchAreaEffect;

    public override void ExecuteEffect()
    {
        Instantiate(punchAreaEffect, transform.position, Quaternion.identity);
        base.ExecuteEffect();
    }
}

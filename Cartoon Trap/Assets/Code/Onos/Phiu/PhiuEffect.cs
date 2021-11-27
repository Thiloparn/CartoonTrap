using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhiuEffect : OnomaopeyaEffect
{
    public override void ExecuteEffect()
    {
        PlayerController player = GameData.player;
        player.Heal();
        base.ExecuteEffect();
    }
}

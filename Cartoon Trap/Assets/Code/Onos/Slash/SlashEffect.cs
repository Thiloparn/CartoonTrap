using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashEffect : OnomaopeyaEffect
{

    public override void InstantiateAreaEffect()
    {
        GameObject slashArea = Instantiate(ono.onoAreaEffect, transform.position, Quaternion.identity);
        PlayerController player = GameData.player;
        if (player.LookingAtDirection() < 0)
        {
            slashArea.transform.Rotate(new Vector3(0, 180, 0));
        }
        else
        {
            slashArea.transform.Rotate(new Vector3(0, 0, 0));
        }
    }
}

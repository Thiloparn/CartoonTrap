using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : IAction
{

    public override void ExecuteAction(PlayerController player)
    {
        positionOfInstanciation = player.transform.position;
        InstantiateOnomatopeya(player.attackOnomatopeya);
    }
}

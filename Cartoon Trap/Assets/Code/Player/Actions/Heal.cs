using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : IAction
{
    private int maxHealings;
    private int currentHealings;

    public Heal(int maxHealings)
    {
        this.maxHealings = maxHealings;
        currentHealings = this.maxHealings;
    }

    public override void ExecuteAction(PlayerController player)
    {
        if (currentHealings > 0)
        {
            positionOfInstanciation = player.transform.position;
            InstantiateOnomatopeya(player.phiuOnomatopeya);
            --currentHealings;
        }

        player.playerAnimator.StartHealingAnimation(player);
    }

    public void resetHealings()
    {
        currentHealings = maxHealings;
    }
}

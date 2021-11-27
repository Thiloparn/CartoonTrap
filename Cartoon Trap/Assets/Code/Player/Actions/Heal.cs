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

    public void ExecuteAction(PlayerController player)
    {
        if (currentHealings > 0)
        {
            MonoBehaviour.Instantiate<GameObject>(player.phiuOnomatopeya, player.transform.position, Quaternion.identity);
            --currentHealings;
        }
    }
}

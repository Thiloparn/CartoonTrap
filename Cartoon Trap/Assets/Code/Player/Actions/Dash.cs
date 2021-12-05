using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : IAction
{
    private readonly float dashDirection;

    public Dash(float dashDirection)
    {
        this.dashDirection = dashDirection;
    }

    public override void ExecuteAction(PlayerController player)
    {
        MonoBehaviour.print("Dash");
        player.RigidBody.velocity = Vector2.right * player.dashSpeed * dashDirection;
    }

    public void EndExecuteAction(PlayerController player)
    {
        player.RigidBody.velocity = Vector2.zero;
    }
}

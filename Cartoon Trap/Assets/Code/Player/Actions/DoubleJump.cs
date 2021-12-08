using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : Jump
{
    public override void ExecuteAction(PlayerController player)
    {
        player.RigidBody.velocity = Vector2.zero;
        player.RigidBody.AddForce(Vector2.up * player.doubleJumpForce, ForceMode2D.Impulse);

        positionOfInstanciation = player.transform.position;
        InstantiateOnomatopeya(player.hopOnomatopeya);
    }
}

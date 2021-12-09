using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rebound : IAction
{
    public override void ExecuteAction(PlayerController player)
    {
        Rigidbody2D rigidbody = player.GetComponent<Rigidbody2D>();
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);
        rigidbody.AddForce(Vector2.up * player.reboundForce, ForceMode2D.Impulse);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HopEffect : OnomaopeyaEffect
{
    public override void ExecuteEffect()
    {
        HopOnomatopeya hopOno = GetComponent<HopOnomatopeya>();
        Rigidbody2D playerRigidbody = GameData.player.GetComponent<Rigidbody2D>();

        Vector3 impulseDirection = Vector3.up * hopOno.upPunchImpulse;

        playerRigidbody.velocity = Vector2.zero;
        playerRigidbody.AddForce(impulseDirection, ForceMode2D.Impulse);
        base.ExecuteEffect();
    }
}

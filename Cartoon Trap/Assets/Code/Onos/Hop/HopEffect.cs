using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HopEffect : OnomaopeyaEffect
{
    public override void ExecuteEffect()
    {
        Vector3 hopPosition = gameObject.transform.position;
        Vector3 playerPosition = GameData.player.transform.position;
        HopOnomatopeya hopOno = GetComponent<HopOnomatopeya>();
        Rigidbody2D playerRigidbody = GameData.player.GetComponent<Rigidbody2D>();

        Vector3 impulseDirection = Vector3.zero;
        if (playerPosition.y > hopPosition.y)
        {
            impulseDirection += Vector3.up * hopOno.upPunchImpulse;
        }
        else if (playerPosition.x > hopPosition.x)
        {
            impulseDirection += Vector3.right * hopOno.horizontalPunchImpulse;
        }else if (playerPosition.x < hopPosition.x)
        {
            impulseDirection += Vector3.left * hopOno.horizontalPunchImpulse;
        }

        playerRigidbody.velocity = Vector2.zero;
        playerRigidbody.AddForce(impulseDirection, ForceMode2D.Impulse);
        base.ExecuteEffect();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : IAction
{
    public override void ExecuteAction(PlayerController player)
    {
        player.GetComponent<Rigidbody2D>().AddForce(Vector2.up * player.jumpForce, ForceMode2D.Impulse);
        player.playerAnimator.StartJumpingAnimation(player);
        player.PlayPlayerAudio(player.hopSound);
    }
}

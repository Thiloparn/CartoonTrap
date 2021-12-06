using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator
{

    private void PlayerAnimatorSetBool(PlayerController player, string var, bool value)
    {
        player.GetComponent<Animator>().SetBool(var, value);
    }

    public void UpdateLookingDirection(PlayerController player)
    {
        if (player.MovingDirectionX < 0)
        {
            player.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if(player.MovingDirectionX > 0)
        {
            player.GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    public void StartWalkingAnimation(PlayerController player)
    {
        PlayerAnimatorSetBool(player, "Walking", true);

        if (player.MovingDirectionX < 0)
        {
            PlayerAnimatorSetBool(player, "LookingRight", false);
        }
        else
        {
            PlayerAnimatorSetBool(player, "LookingRight", true);
        }
        
    }

    public void EndWalkingAnimation(PlayerController player)
    {
        PlayerAnimatorSetBool(player, "Walking", false);
    }

    public void StartPunchingAnimation(PlayerController player)
    {
        PlayerAnimatorSetBool(player, "Punching", true);
    }

    public void EndPunchingAnimation(PlayerController player)
    {
        PlayerAnimatorSetBool(player, "Punching", false);
    }
}

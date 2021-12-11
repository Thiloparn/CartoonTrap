using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator
{

    private void PlayerAnimatorSetBool(PlayerController player, string var, bool value)
    {
        player.GetComponent<Animator>().SetBool(var, value);
    }

    private void PlayerAnimatorSetFloat(PlayerController player, string var, float value)
    {
        player.GetComponent<Animator>().SetFloat(var, value);
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

    public void StartPunchingUpAnimation(PlayerController player)
    {
        PlayerAnimatorSetBool(player, "PunchingUp", true);
    }

    public void EndPunchingUpAnimation(PlayerController player)
    {
        PlayerAnimatorSetBool(player, "PunchingUp", false);
    }

    public void StartPunchingDownAnimation(PlayerController player)
    {
        PlayerAnimatorSetBool(player, "PunchingDown", true);
    }

    public void EndPunchingDownAnimation(PlayerController player)
    {
        PlayerAnimatorSetBool(player, "PunchingDown", false);
    }

    public void StartHealingAnimation(PlayerController player)
    {
        PlayerAnimatorSetBool(player, "Healing", true);
    }

    public void EndHealingAnimation(PlayerController player)
    {
        PlayerAnimatorSetBool(player, "Healing", false);
    }

    public void StartUsingHammerAnimation(PlayerController player)
    {
        PlayerAnimatorSetBool(player, "UsingHammer", true);
    }

    public void EndUsingHammerAnimation(PlayerController player)
    {
        PlayerAnimatorSetBool(player, "UsingHammer", false);
    }

    public void StartRestingAnimation(PlayerController player)
    {
        PlayerAnimatorSetBool(player, "Resting", true);
    }

    public void EndRestingAnimation(PlayerController player)
    {
        PlayerAnimatorSetBool(player, "Resting", false);
    }

    public void StartUsingBladeAnimation(PlayerController player)
    {
        PlayerAnimatorSetBool(player, "UsingBlade", true);
    }

    public void EndUsingBladeAnimation(PlayerController player)
    {
        PlayerAnimatorSetBool(player, "UsingBlade", false);
    }

    public void StartDashingAnimation(PlayerController player)
    {
        PlayerAnimatorSetBool(player, "Dashing", true);
    }

    public void EndDashingAnimation(PlayerController player)
    {
        PlayerAnimatorSetBool(player, "Dashing", false);
    }

    public void StartHurtingAnimation(PlayerController player)
    {
        PlayerAnimatorSetBool(player, "Hurting", true);
    }

    public void EndHurtingAnimation(PlayerController player)
    {
        PlayerAnimatorSetBool(player, "Hurting", false);
    }

    public void StartDyingAnimation(PlayerController player)
    {
        PlayerAnimatorSetBool(player, "Dead", true);
    }

    public void EndDyingAnimation(PlayerController player)
    {
        PlayerAnimatorSetBool(player, "Dead", false);
    }

    public void StartGrappingAnimation(PlayerController player)
    {
        PlayerAnimatorSetBool(player, "Grapping", true);
    }

    public void EndGrappingAnimation(PlayerController player)
    {
        PlayerAnimatorSetBool(player, "Grapping", false);
    }

    public void StartJumpingAnimation(PlayerController player)
    {
        PlayerAnimatorSetBool(player, "Jumping", true);
    }

    public void EndJumpingAnimation(PlayerController player)
    {
        PlayerAnimatorSetBool(player, "Jumping", false);
    }

    public void UpdateVelocityY(PlayerController player)
    {
        PlayerAnimatorSetFloat(player, "VelocityY", player.RigidBody.velocity.y);
    }
}

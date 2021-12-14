using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestZone : MonoBehaviour
{
    private PlayerController player = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.gameObject.GetComponent<PlayerController>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player = null;
    }

    private void Update()
    {
        if (player != null)
        {
            if (player.MovingDirectionY > 0)
            {
                player.Rest();
                UpdateGameData(player);
            }
        }
    }

    private void UpdateGameData(PlayerController player)
    {
        //Rest
        GameData.lastRestZone = GameData.lastRestZone = gameObject.transform.position;;

        //Player
        GameData.punchLocked = true;
        GameData.slashLocked = true;
        GameData.pumLocked = true;
        GameData.phiuLocked = true;
        GameData.hopLocked = true;
        GameData.r = player.r;
        GameData.g = player.g;
        GameData.b = player.b;
        GameData.maxPlayerHealth = player.PlayerHealth.MaxHealth;
        GameData.currentPlayerHealth = player.PlayerHealth.CurrentHealth;
        GameData.numberOfHealings = player.numberOfHealings;
        GameData.attackPower = player.attackPower;
        GameData.coins = player.Coins;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            player.Rest();
            UpdateGameData(player);
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
        GameData.maxPlayerHealt = player.PlayerHealth.MaxHealth;
        GameData.currentPlayerHealth = player.PlayerHealth.CurrentHealth;
        GameData.attackPower = player.attackPower;
        GameData.coins = player.Coins;
    }
}

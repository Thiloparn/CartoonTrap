using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    //Rest
    public static Vector3 NO_REST_ZONE = Vector3.negativeInfinity;
    public static Vector3 lastRestZone = NO_REST_ZONE;

    //Player
    public static PlayerController player = null;
    public static Vector3 playerAparitionPosition = Vector3.zero;
    public static bool punchLocked = true;
    public static bool slashLocked = true;
    public static bool pumLocked = true;
    public static bool phiuLocked = true;
    public static bool hopLocked = true;
    public static bool r = false;
    public static bool g = false;
    public static bool b = false;
    public static int maxPlayerHealth = 10;
    public static int numberOfHealings = 3;
    public static int currentPlayerHealth = 10;
    public static int attackPower = 10;
    public static int coins = 0;

    //Final Door
    public static bool finalDoorOpen = false;
}

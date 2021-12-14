using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    public Image StatusVida;
    public Image StatusVida2;
    public Image StatusVida3;

    public PlayerController player;
    public Health playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        StatusVida.enabled = false;
        StatusVida2.enabled = false;
        StatusVida3.enabled = false;
        //playerHealth = GameObject.FindGameObjectWithTag("PlayerPrefab").GetComponent<Health>();
        switch (player.playerHealth.CurrentHealth)
        {
            case 3:
                StatusVida3.enabled = true;
                StatusVida2.enabled = true;
                StatusVida.enabled = true;
                break;
            case 2:
                StatusVida2.enabled = true;
                StatusVida.enabled = true;
                break;
            case 1:
                StatusVida.enabled = true;
                break;
            case 0:
                break;

        }
    }

    // Update is called once per frame
    void Update()
    {
        StatusVida.enabled = false;
        StatusVida2.enabled = false;
        StatusVida3.enabled = false;
        //playerHealth = GameObject.FindGameObjectWithTag("PlayerPrefab").GetComponent<Health>();
        switch (player.playerHealth.CurrentHealth)
        {
            case 3:
                StatusVida3.enabled = true;
                break;
            case 2:
                StatusVida2.enabled = true;
                break;
            case 1:
                StatusVida.enabled = true;
                break;
            case 0:
                break;

        }
    }
}

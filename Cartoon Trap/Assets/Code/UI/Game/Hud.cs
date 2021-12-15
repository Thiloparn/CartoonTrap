using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    public Image StatusVida;
    public Image StatusVida2;
    public Image StatusVida3;

    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameData.player;
        UpdateLife();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLife();
    }

    private void UpdateLife()
    {
        StatusVida.enabled = false;
        StatusVida2.enabled = false;
        StatusVida3.enabled = false;

        //print(player.PlayerHealth.CurrentHealth);

        switch (player.PlayerHealth.CurrentHealth)
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

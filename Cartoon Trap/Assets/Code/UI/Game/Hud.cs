using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    public Image StatusVida;
    public Image StatusVida2;
    public Image StatusVida3;

    public Health playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("PlayerPrefab").GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

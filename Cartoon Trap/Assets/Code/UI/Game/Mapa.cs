using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mapa : MonoBehaviour
{
    [SerializeField] GameObject map;
    bool mapisShown;

    private void Awake()
    {
        mapisShown = false;
        map.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (mapisShown)
            {
                hideMap();
            }
            else
            {
                showMap();
            }
        }else if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (mapisShown)
            {
                hideMap();
            }
        }

    public void showMap()
    {
        map.SetActive(true);
        Time.timeScale = 0f;
        mapisShown = true;
    }
    public void hideMap()
    {
        map.SetActive(false);
        mapisShown = false;
        Time.timeScale = 1f;

    }
}

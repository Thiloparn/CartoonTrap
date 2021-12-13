using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Mapa : MonoBehaviour
{
    [SerializeField] GameObject map;
    bool mapisShown;

    [SerializeField] PlayerInput playerInput;

    private void Awake()
    {
        mapisShown = false;
        map.SetActive(false);
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
    public void onPause(InputAction.CallbackContext value)
    {
        if (mapisShown)
        {
            hideMap();
        }
    }
    public void onMenu(InputAction.CallbackContext value)
    {
        if (mapisShown)
        {
            hideMap();
        }
        else
        {
            showMap();
        }
    }
}

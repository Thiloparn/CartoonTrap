using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenDialogues : MonoBehaviour
{
    [SerializeField] string[] dialogueText;
    [SerializeField] GameObject dialogueUI;
    [SerializeField] Text dialogueField;
    bool active;

    public void dialogue(int start, int num)
    {
        Time.timeScale = 0f;
        dialogueUI.SetActive(true);
        for (int i = 0; i<num; i++)
        {
            bool wait = true;
            dialogueField.text = dialogueText[i];
            while(wait)
            {
                if(Input.GetKeyDown(KeyCode.A) | Input.GetKeyDown(KeyCode.KeypadEnter) | Input.GetKeyDown(KeyCode.Space))
                {
                    wait = false;
                }
            }
        }
        dialogueUI.SetActive(false);
        Time.timeScale = 1f;

    }
    private void Awake()
    {
        dialogueUI.SetActive(false);
        active = false;
    }
    private void Update()
    {
        if(active == false)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                active = true;
                dialogue(0, 3);
            }
        }
        
    }
}

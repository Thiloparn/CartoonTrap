using System;
using UnityEngine;
using UnityEngine.UI;

public class InteractableObject : MonoBehaviour
{
    public Texts texts;
    public Sprite image;
    public DialogueController dialogueCotroller;
    public GameObject dialogueUI;

    public void CallDialogue()
    {
        print("ButtonPressed");
        dialogueUI.SetActive(true);
        FindObjectOfType<DialogueController>().ActivateDialogue(texts, image);
    }
}

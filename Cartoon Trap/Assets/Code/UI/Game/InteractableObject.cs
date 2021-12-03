using System;
using UnityEngine;
using UnityEngine.UI;

public class InteractableObject : MonoBehaviour
{
    public Texts texts;
    public Sprite image;

    public void callDialogue()
    {
        Console.WriteLine("ButtonPressed");
        FindObjectOfType<DialogueController>().ActivateDialogue(texts, image);
    }
}

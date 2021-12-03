using System;
using UnityEngine;
using UnityEngine.UI;

public class InteractableObject : MonoBehaviour
{
    public Texts texts;
    public Sprite image;

    public void OnMouseDown()
    {
        print("ButtonPressed");
        FindObjectOfType<DialogueController>().ActivateDialogue(texts, image);
    }
}

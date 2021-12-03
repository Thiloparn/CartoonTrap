using System;
using UnityEngine;
using UnityEngine.UI;

public class InteractableObject : MonoBehaviour
{
    public Texts texts;
    public Sprite image;
    public GameObject dialoueCotroller;

    public void CallDialogue()
    {
        print("ButtonPressed");
        dialoueCotroller.SetActive(true);
        FindObjectOfType<DialogueController>().ActivateDialogue(texts, image);
    }
}

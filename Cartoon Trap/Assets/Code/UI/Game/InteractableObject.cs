using System;
using UnityEngine;
using UnityEngine.UI;

public class InteractableObject : MonoBehaviour
{
    public Texts texts;
    public Sprite image;
    public GameObject dialogueCotroller;

    public void CallDialogue()
    {
        dialogueCotroller.SetActive(true);
        FindObjectOfType<DialogueController>().ActivateDialogue(texts, image);
    }
}

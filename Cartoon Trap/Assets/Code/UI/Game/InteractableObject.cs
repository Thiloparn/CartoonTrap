using System;
using UnityEngine;
using UnityEngine.UI;

public class InteractableObject : MonoBehaviour
{
    public Texts texts;
    public Sprite image;
    public GameObject dialogueController;

    public void CallDialogue()
    {
        dialogueController.SetActive(true);
        FindObjectOfType<DialogueController>().ActivateDialogue(texts, image);
    }
}

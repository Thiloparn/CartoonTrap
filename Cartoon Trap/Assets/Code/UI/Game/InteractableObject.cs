using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableObject : MonoBehaviour
{
    public Texts texts;
    public Sprite image;

    private void OnMouseDown()
    {
        FindObjectOfType<DialogueController>().ActivateDialogue(texts, image);
    }
}

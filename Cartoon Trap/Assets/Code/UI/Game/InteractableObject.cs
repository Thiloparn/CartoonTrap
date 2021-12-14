using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] CharacterDialogue[] dialogueArray;
    public DialogueController dialogueCotroller;
    public GameObject dialogueUI;

    public void CallDialogue()
    {
        print("ButtonPressed");
        dialogueUI.SetActive(true);
        foreach(CharacterDialogue dialogue in dialogueArray)
        {
            FindObjectOfType<DialogueController>().ActivateDialogue(dialogue.text, dialogue.image);
            StartCoroutine(CountDown(0.5f));
            while (FindObjectOfType<DialogueController>().isTextWaiting()){
                StartCoroutine(CountDown(0.5f));
            }
        }
        FindObjectOfType<DialogueController>().CloseDialogue();


    }
    IEnumerator CountDown(float RestartAfter)
    {
        yield return new WaitForSeconds(RestartAfter);
    }
}

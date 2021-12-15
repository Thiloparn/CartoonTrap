using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] CharacterDialogue[] dialogueArray;
    public DialogueController dialogueCotroller;
    public GameObject dialogueUI;
    private Queue<CharacterDialogue> dialogueQueue;
    private bool activeUpdate = false;
    public new BoxCollider2D collider;
    private bool wait = false;

    void Awake()
    {
        dialogueQueue = new Queue<CharacterDialogue>();
        activeUpdate = false;
    }

    public void CallDialogue()
    {
        dialogueQueue.Clear();
        dialogueUI.SetActive(true);
        foreach (CharacterDialogue characterDialogue in dialogueArray)
        {
            dialogueQueue.Enqueue(characterDialogue);
        }
        NextDialogue();
    }
    public void NextDialogue()
    {
        if (dialogueQueue.Count == 0)
        {
            FindObjectOfType<DialogueController>().CloseDialogue();
            activeUpdate = false;
            Destroy(gameObject);
            return;
        }
        CharacterDialogue characterDialogue = dialogueQueue.Dequeue();
        print("Text:" + characterDialogue.text.arrayTextos[0].ToString());
        FindObjectOfType<DialogueController>().ActivateDialogue(characterDialogue.text, characterDialogue.image);
        activeUpdate = true;
    }
    IEnumerator CountDown(float RestartAfter)
    {
        wait = true;
        yield return new WaitForSeconds(RestartAfter);
        wait = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collider.enabled = false;
        CallDialogue();
    }

    private void FixedUpdate()
    {
        if (activeUpdate && !wait)
        {
            if (!FindObjectOfType<DialogueController>().isTextWaiting())
            {
                NextDialogue();
            }
        }   
    }
}

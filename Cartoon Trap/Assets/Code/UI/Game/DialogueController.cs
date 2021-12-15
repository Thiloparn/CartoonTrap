using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    private Animator anim;
    private Queue <string> dialogueQueue;
    Texts texts;
    [SerializeField] Text screenText;
    [SerializeField] Image imageField;
    [SerializeField] GameObject dialogueGUI;
    private bool wait = false;
    private bool textWaiting = false;
    private bool first = true;

    void Awake()
    {
        first = true;
        anim = GetComponent<Animator>();
        dialogueGUI.SetActive(false);
        dialogueQueue = new Queue<string>();
    }
    public void ActivateDialogue(Texts textsObject, Sprite image)
    {
        if (first)
        {
            anim.SetBool("Bocadillo", true);
        }
        first = false;
        textWaiting = true;
        texts = textsObject;
        imageField.sprite = image;
        ActivateText();
       
    }

    public void ActivateText()
    {
        dialogueQueue.Clear();
        foreach (string text in texts.arrayTextos)
        {
            dialogueQueue.Enqueue(text);
        }
        NextDialogue();
    }
    public void NextDialogue()
    {
        wait = true;
        if (dialogueQueue.Count == 0)
        {
            textWaiting = false;           
            return;
        }
        string dialogue = dialogueQueue.Dequeue();
        screenText.text = dialogue;
        StartCoroutine(ShowCharacters(dialogue));
    }

    IEnumerator ShowCharacters (string text)
    {
        screenText.text = "";
        foreach (char character in text.ToCharArray())
        {
            screenText.text += character;
            yield return new WaitForSeconds(0.02f);
        }
        wait = false;
    }
    IEnumerator CountDown(float RestartAfter)
    {
        wait = true;
        yield return new WaitForSeconds(RestartAfter);
        wait = false;
    }

    public void CloseDialogue()
    {
        anim.SetBool("Bocadillo", false);
    }

    public void DeactivateDialogue()
    {
        screenText.text = "";
        dialogueGUI.SetActive(false);
    }
    public void onJumpOrAttack()
    {
        if (!wait && isDIalogueActive())
        {
            NextDialogue();
        }      
    }
    public bool isDIalogueActive()
    {
        return dialogueGUI.gameObject.activeInHierarchy;
    }
    public bool isTextWaiting()
    {
        return textWaiting;
    }
}

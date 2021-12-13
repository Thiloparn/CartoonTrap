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
    private bool active = false;

    void Awake()
    {
        anim = GetComponent<Animator>();
        dialogueGUI.SetActive(false);
        dialogueQueue = new Queue<string>();
    }
    public void ActivateDialogue(Texts textsObject, Sprite image)
    {
        active = true;
        dialogueGUI.SetActive(true);
        anim.SetBool("Bocadillo", true);
        texts = textsObject;
        imageField.sprite = image;
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
        if (dialogueQueue.Count == 0)
        {
            screenText.text = "";
            anim.SetBool("Bocadillo", false);
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
    }

    void CloseDialogue()
    {
        dialogueGUI.SetActive(false);
    }
    private void Update()
    {
        if (active)
        {
            if (Input.GetKeyDown(KeyCode.A) | Input.GetKeyDown(KeyCode.KeypadEnter) | Input.GetKeyDown(KeyCode.Space))
            {
                NextDialogue();
            }
        }
    }
}

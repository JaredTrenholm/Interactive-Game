using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject player;
    public Animator animator;
    public GameObject DialogueUI;
    public Text DialogueText;

    private Queue<string> dialogueQueue;

    private void Start()
    {
        dialogueQueue = new Queue<string>();
    }

    public void StartDialogue(string[] sentences)
    {
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        animator.SetFloat("Speed", 0);
        player.GetComponent<PlayerMovement_2D>().enabled = false;
        player.GetComponent<PlayerInteraction>().enabled = false;
        dialogueQueue.Clear();
        DialogueUI.SetActive(true);

        foreach (string currentline in sentences)
        {
            dialogueQueue.Enqueue(currentline);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (dialogueQueue.Count == 0)
        {
            EndDialogue();
            return;
        }
        string currentline = dialogueQueue.Dequeue();
        DialogueText.text = currentline;
    }

    public void EndDialogue()
    {
        dialogueQueue.Clear();
        DialogueUI.SetActive(false);
        player.GetComponent<PlayerMovement_2D>().enabled = true;
        player.GetComponent<PlayerInteraction>().enabled = true;

        player.GetComponent<PlayerInteraction>().CheckDialogueCondition();
    }
}

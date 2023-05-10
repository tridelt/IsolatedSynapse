using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public Message[] messages;
    public Actor[] actors;
    private bool playerInRange = false;
    private bool dialogActive = false;
    public bool needInput = true;
    public bool executed = false;

    void Update()
    {
        if (playerInRange && !DialogManager.isActive)
        {
            if (needInput && Input.GetKeyDown(KeyCode.F))
            {
                StartDialog();
            }
        }
    }

    public void StartDialog()
    {
        FindObjectOfType<DialogManager>().OpenDialogue(messages, actors);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (needInput)
            {
                playerInRange = true;
            }
            else if (!executed)
            {
                StartDialog();
                executed = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}

[System.Serializable]
public class Message
{
    public int actorId;
    public string message;
}

[System.Serializable]
public class Actor
{
    public string name;
    public Sprite sprite;
}

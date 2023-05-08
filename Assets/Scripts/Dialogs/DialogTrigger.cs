using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public Message[] messages;
    public Actor[] actors;
    private bool playerInRange = false;
    private bool dialogActive = false;

    void Update()
    {
        // if (playerInRange && Input.GetKeyUp(KeyCode.F) && !dialogActive)
        // {
        //     StartDialog(); // Trigger dialog if player is in range and F key is pressed
        //     dialogActive = true;
        // }
        // else if (Input.GetKeyUp(KeyCode.F) && dialogActive)
        // {
        //     FindObjectOfType<DialogManager>().NextMessage();
        // }
    }

    public void StartDialog()
    {
        FindObjectOfType<DialogManager>().OpenDialogue(messages, actors);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartDialog();
            playerInRange = true;
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

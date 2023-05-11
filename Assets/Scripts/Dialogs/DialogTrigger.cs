using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogTrigger : MonoBehaviour
{
    public Message[] messages;
    public Actor[] actors;
    public bool needInput = true;

    private bool playerInRange = false;
    private bool dialogActive = false;
    private bool executed = false;
    PlayerControls PlayerInput; // Input System for the player controls

    void Update()
    {
        // if (playerInRange && !DialogManager.isActive)
        // {
        //     if (needInput && Input.GetKeyDown(KeyCode.F))
        //     {
        //         StartDialog();
        //     }
        // }
    }

    void Awake()
    {
        PlayerInput = new PlayerControls();
        PlayerInput.Enable();
        PlayerInput.Player.Interact.performed += StartDialog;
    }

    public void StartDialog(InputAction.CallbackContext context)
    {
        if (playerInRange && !DialogManager.isActive)
        {
            FindObjectOfType<DialogManager>().OpenDialogue(messages, actors);
        }
    }

    public void StartDialogTwo()
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
                StartDialogTwo();
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

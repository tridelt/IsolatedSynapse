using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class DialogManager : MonoBehaviour
{
    public TextMeshProUGUI actorName;
    public TextMeshProUGUI dialogText;
    public RectTransform dialogBox;

    Message[] currentMessages;
    Actor[] currentActors;
    int activeMessage = 0;
    public static bool isActive = false;
    public static bool dialogFinished = false;

    PlayerControls PlayerInput; // Input System for the player controls

    public void OpenDialogue(Message[] messages, Actor[] actors)
    {
        currentMessages = messages;
        currentActors = actors;
        activeMessage = 0;
        isActive = true;
        dialogFinished = false;
        dialogBox.transform.localScale = new Vector3(1, 1, 1);
        DisplayMessage();
    }

    void DisplayMessage()
    {
        Message message = currentMessages[activeMessage];
        Actor actor = currentActors[message.actorId];
        actorName.text = actor.name;
        dialogText.text = message.message;
    }

    public void NextMessage(InputAction.CallbackContext context)
    {
        activeMessage++;
        if (activeMessage < currentMessages.Length)
        {
            DisplayMessage();
        }
        else
        {
            isActive = false;
            dialogFinished = true;
            dialogBox.transform.localScale = new Vector3(0, 0, 0);
        }
    }

    void Start()
    {
        dialogBox.transform.localScale = new Vector3(0, 0, 0);
    }

    private void Awake()
    {
        PlayerInput = new PlayerControls();
        PlayerInput.Enable();
        PlayerInput.Player.Dodge.performed += NextMessage;
    }
}

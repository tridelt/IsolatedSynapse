using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public TextMeshProUGUI actorName;
    public TextMeshProUGUI dialogText;
    public RectTransform dialogBox;

    Message[] currentMessages;
    Actor[] currentActors;
    int activeMessage = 0;
    public static bool isActive = false;

    public void OpenDialogue(Message[] messages, Actor[] actors)
    {
        currentMessages = messages;
        currentActors = actors;
        activeMessage = 0;
        isActive = true;
        dialogBox.gameObject.SetActive(true);
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

    public void NextMessage()
    {
        activeMessage++;
        if (activeMessage < currentMessages.Length)
        {
            DisplayMessage();
        }
        else
        {
            isActive = false;
            dialogBox.transform.localScale = new Vector3(0, 0, 0);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        dialogBox.transform.localScale = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.F) && isActive)
        {
            NextMessage();
        }
    }
}

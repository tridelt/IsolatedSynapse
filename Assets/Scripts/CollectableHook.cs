using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableHook : MonoBehaviour
{
    public float amplitude = 0.2f;
    private Vector3 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = initialPosition;
        newPosition.y += Mathf.Sin(Time.time) * amplitude;
        transform.position = newPosition;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerScript>().HookCollected();
            DisplayDialogHook();
            Destroy(gameObject);
        }
    }

    void DisplayDialogHook()
    {
        Actor[] actors = new Actor[1];
        Message[] messages = new Message[1];
        actors[0] = new Actor();
        actors[0].name = "Selene";
        messages[0] = new Message();
        messages[0].actorId = 0;
        messages[0].message = "Here it is, my trusty hook. Now I'm prepared for anything!";

        FindObjectOfType<DialogManager>().OpenDialogue(messages, actors);
    }
}

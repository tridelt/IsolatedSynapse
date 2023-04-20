using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Rune : MonoBehaviour
{
    public SpriteRenderer _spriteRenderer;
    public UnityEvent onStateChanged; // Event to be triggered when the state is changed

    private bool runeIsActive = false; // Initial state of the object
    private int currentPosition = 0;
    public int triggerPosition = 60;
    public string runeName;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ModifyState()
    {
        onStateChanged.Invoke();
    }

    private void RotateRune()
    {
        currentPosition += 30;
        transform.Rotate(0, 0, currentPosition);

        if (currentPosition == triggerPosition)
        {
            _spriteRenderer.color = Color.green;
            runeIsActive = true;
            ModifyState();
        }
        else
        {
            _spriteRenderer.color = Color.white;
            runeIsActive = false;
            ModifyState();
        }
    }

    public void Triggered()
    {
        RotateRune();
        // Debug.Log("Triggered");
    }
}
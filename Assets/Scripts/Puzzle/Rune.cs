using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Rune : MonoBehaviour
{
    public AudioClip trap;
    AudioSource audio;
    public SpriteRenderer _spriteRenderer;
    public UnityEvent onStateChanged; // Event to be triggered when the state is changed

    public bool runeIsActive = false; // Initial state of the object
    public int currentPosition;
    public int triggerPosition = 90;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        ModifyState();
    }

    public void ModifyState()
    {
        if (currentPosition == triggerPosition)
        {
            // _spriteRenderer.color = Color.green;
            runeIsActive = true;
        }
        else
        {
            // _spriteRenderer.color = Color.white;
            runeIsActive = false;
        }

        onStateChanged.Invoke();
    }

    private void RotateRune()
    {
        audio.clip = trap;
        audio.Play();
        currentPosition = (currentPosition + 90) % 360;
        transform.Rotate(0, 0, 90);
        ModifyState();
    }

    public void Triggered()
    {
        RotateRune();
    }
}
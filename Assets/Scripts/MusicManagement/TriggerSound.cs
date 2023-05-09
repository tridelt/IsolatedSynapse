using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSound : MonoBehaviour
{
    public AudioClip clip;
    AudioSource audioSource;
    private bool _triggered = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_triggered)
        {
            return;
        }

        _triggered = true;
        audioSource.clip = clip;
        audioSource.Play();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public AudioClip trap;
    AudioSource audio;
    public Transform blockade_east;
    public Transform blockade_south;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        blockade_east.gameObject.SetActive(true);
        blockade_south.gameObject.SetActive(true);
        audio.clip = trap;
        audio.Play();
    }
}
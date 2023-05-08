using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Manager : MonoBehaviour
{
    public int globalOder = 0;
    public AudioClip trap;
    AudioSource audio;
    public Transform key;
    public bool isBlocked = false;
    public UnityEvent onStateChanged; // Event to be triggered when the state is changed
    public int nCorrectTiles;
    public int foundCorrectTiles = 0;
    public bool puzzleSolved = false;

    void Awake()
    {
        Tile[] tiles = FindObjectsOfType<Tile>();
        foreach (Tile tile in tiles)
        {
            tile.onStateChanged.AddListener(() => OnTilesStateChange(tile));
        }

        ResetArea resetArea = FindObjectsOfType<ResetArea>()[0];
        resetArea.onStateChanged.AddListener(() => OnResetStateChange());
    }

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }


    private void OnTilesStateChange(Tile tile)
    {
        if (tile.order != globalOder)
        {
            isBlocked = true;
        }
        else
        {
            globalOder += 1;
            foundCorrectTiles += 1;
            if (foundCorrectTiles == nCorrectTiles)
            {
                puzzleSolved = true;
                audio.clip = trap;
                audio.Play();
                key.gameObject.SetActive(true);
            }
        }
        onStateChanged.Invoke();
    }

    private void OnResetStateChange()
    {
        if (isBlocked)
        {
            globalOder = 0;
            isBlocked = false;
            foundCorrectTiles = 0;
            onStateChanged.Invoke();
        }
    }
}
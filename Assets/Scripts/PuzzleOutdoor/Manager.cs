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
    public bool isPuzzleSolved = false;

    GameObject GameManager;

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
        GameManager = GameObject.Find("GameManager");
        // SetPuzzleSolved();
    }
    
    public void SetPuzzleSolved(bool key_obtained)
    {
        isPuzzleSolved = true;
        // for all tiles, set them to be blocked
        Tile[] tiles = FindObjectsOfType<Tile>();
        foreach (Tile tile in tiles)
        {
            tile._alreadyTriggered = true;
            tile._spriteRenderer.color = new Color(0.309682f, 0.6415094f, 0.3056248f, 0.5f);
        }
        key.gameObject.SetActive(!key_obtained);
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
                isPuzzleSolved = true;
                audio.clip = trap;
                audio.Play();
                key.gameObject.SetActive(true);
                GameManager.GetComponent<GameManager>().OpenWorldPuzzleCompleted();
            }
        }
    }

    private void OnResetStateChange()
    {
        if (isBlocked)
        {
            globalOder = 0;
            isBlocked = false;
            foundCorrectTiles = 0;
        }
    }
}
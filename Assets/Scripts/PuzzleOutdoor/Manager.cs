using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Manager : MonoBehaviour
{
    public bool isBlocked = false;
    public UnityEvent onStateChanged; // Event to be triggered when the state is changed
    public int nCorrectTiles = 3;
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
    }

    void Update()
    {
    }

    private void OnTilesStateChange(Tile tile)
    {
        if (!tile.amIaGoodTile)
        {
            isBlocked = true;
            onStateChanged.Invoke();
        }
        else
        {
            foundCorrectTiles += 1;
            if (foundCorrectTiles == nCorrectTiles)
            {
                puzzleSolved = true;
                onStateChanged.Invoke();
            }
        }
    }

    private void OnResetStateChange()
    {
        if (isBlocked)
        {
            isBlocked = false;
            foundCorrectTiles = 0;
            onStateChanged.Invoke();
            Debug.Log("manager resets tiles");
        }
    }
}
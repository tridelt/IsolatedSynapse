using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Tile : MonoBehaviour
{
    // private Manager _manager = GameObject.Find("Manager").GetComponent<Manager>();

    private Manager _manager;
    public int order;
    public bool _alreadyTriggered = false;
    public SpriteRenderer _spriteRenderer;
    public UnityEvent onStateChanged; // Event to be triggered when the state is changed


    private void Awake()
    {
        _manager = FindObjectsOfType<Manager>()[0];
        ResetArea resetArea = FindObjectsOfType<ResetArea>()[0];
        resetArea.onStateChanged.AddListener(() => OnResetAreaChange());
    }

    void Start()
    {
        // if (gameObject.name == "Square (4)")
        // { 
        //     // print all variables 
        //     print("order: " + order);
        //     print("_alreadyTriggered: " + _alreadyTriggered);
        //     print("_isBlocked: " + _isBlocked);
        //     
        // }

        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (_alreadyTriggered || _manager.isBlocked)
        {
            return;
        }


        if (order == _manager.globalOder)
        {
            _spriteRenderer.color = new Color(0.309682f, 0.6415094f, 0.3056248f, 0.5f);
        }
        else
        {
            _spriteRenderer.color = new Color(0.7075472f, 0.21f, 0.21f, 0.5f);
        }

        _alreadyTriggered = true;
        onStateChanged.Invoke();
    }


    private void OnResetAreaChange()
    {
        if (!_manager.isPuzzleSolved)
        {
            _alreadyTriggered = false;
            _spriteRenderer.color = new Color(0f, 0f, 0f, 0f);
        }
    }
}
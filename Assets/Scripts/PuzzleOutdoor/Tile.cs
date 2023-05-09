using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Tile : MonoBehaviour
{
    private int _globalOrder = 0;
    public int order;
    public bool _alreadyTriggered = false;
    public bool _isBlocked = false;
    public SpriteRenderer _spriteRenderer;
    public UnityEvent onStateChanged; // Event to be triggered when the state is changed


    private void Awake()
    {
        Manager manager = FindObjectsOfType<Manager>()[0];
        manager.onStateChanged.AddListener(() => OnManagerStateChange(manager));

        ResetArea resetArea = FindObjectsOfType<ResetArea>()[0];
        resetArea.onStateChanged.AddListener(() => OnResetAreaChange());
    }

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (_alreadyTriggered || _isBlocked)
        {
            return;
        }


        if (order == _globalOrder)
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


    private void OnManagerStateChange(Manager manager)
    {
        _isBlocked = manager.isBlocked;
        _globalOrder = manager.globalOder;
    }

    private void OnResetAreaChange()
    {
        if (_isBlocked)
        {
            _alreadyTriggered = false;
            _spriteRenderer.color = new Color(0f, 0f, 0f, 0f);
        }
    }
}
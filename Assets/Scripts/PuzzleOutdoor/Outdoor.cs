using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outdoor : MonoBehaviour
{
    SpriteRenderer _spriteRenderer;

    // Start is called before the first frame update
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
        _spriteRenderer.color = Color.blue;
    }
}
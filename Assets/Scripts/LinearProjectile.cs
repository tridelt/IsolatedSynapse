using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class LinearProjectile : MonoBehaviour
{
    public float speed;
    private Transform player_pos;
    private bool hitPlayer = false;

    void Start()
    {
        player_pos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (!hitPlayer)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                player_pos.position,
                speed * Time.deltaTime
            );
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            hitPlayer = true;
            other.GetComponent<PlayerScript>().TakeDamage(20);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Column"))
        {
            Destroy(gameObject);
        }
    }
}

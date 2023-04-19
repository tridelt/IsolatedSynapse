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
            transform.position = Vector3.MoveTowards(
                transform.position,
                player_pos.position,
                speed * Time.deltaTime
            );

            Console.WriteLine(player_pos.position);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            hitPlayer = true;
            Destroy(gameObject);
        }
    }
}

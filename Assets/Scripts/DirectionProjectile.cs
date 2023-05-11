using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class DirectionProjectile : MonoBehaviour
{
    public float speed;
    public float direction;
    public float angle;
    private bool hitPlayer = false;

    public void SetValues(float speed, float angle, float direction)
    {
        this.speed = speed;
        this.angle = angle;
        this.direction = direction;
    }

    void Update()
    {
        if (!hitPlayer)
        {
            float angleRad = angle * Mathf.Deg2Rad;
            float dirX = Mathf.Cos(angleRad) * direction;
            float dirY = Mathf.Sin(angleRad);
            Vector2 directionVector = new Vector2(dirX, dirY);
            Vector2 movement = directionVector * speed;
            transform.Translate(movement * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            hitPlayer = true;
            other.GetComponent<PlayerScript>().TakeDamage(3);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Column"))
        {
            Destroy(gameObject);
        }
    }
}

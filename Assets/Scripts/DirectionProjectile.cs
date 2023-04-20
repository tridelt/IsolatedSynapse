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
            // Convertir el ángulo en radianes
            float angleRad = angle * Mathf.Deg2Rad;

            // Calcular los componentes X e Y del vector de dirección
            float dirX = Mathf.Cos(angleRad) * direction;
            float dirY = Mathf.Sin(angleRad);

            // Crear el vector de dirección
            Vector2 directionVector = new Vector2(dirX, dirY);

            // Multiplicar el vector de dirección por la velocidad de la bala
            Vector2 movement = directionVector * speed;

            // Mover la bala usando el vector de movimiento
            transform.Translate(movement * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            hitPlayer = true;
            Destroy(gameObject);
        }
        else if (other.CompareTag("Column"))
        {
            Destroy(gameObject);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float movementSpeed = 3.0f;
    Vector2 movement = new Vector2();
    Rigidbody2D rb2d;
    private BoxCollider2D _box;

    void Start()
    {
        // Debug.Log("hola!");

        rb2d = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Collision occurred! with enemy");
        }
    }

    // private void OnDrawGizmos()
    // {
    //     print("Drawing Gizmos!");
    //     Gizmos.color = Color.blue;
    //     Gizmos.DrawSphere(Vector3.zero, 1f);
    //     if (_box != null)
    //     {
    //         Gizmos.color = Color.red;
    //         var (corner1, corner2) = getGroundCheckCorners();
    //         Debug.Log(corner1);
    //         Debug.Log(corner2);
    //         Gizmos.DrawCube((corner1 + corner2 + corner2) * 0.5f, corner2 - corner1);
    //     }
    // }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = Vector2.ClampMagnitude(movement, 1.0f);
        rb2d.velocity = movement * movementSpeed;
    }

    private (Vector2, Vector2) getGroundCheckCorners()
    {
        Vector3 max = _box.bounds.max;
        Vector3 min = _box.bounds.min;
        Vector2 corner1 = new Vector2(max.x, min.y - .1f);
        Vector2 corner2 = new Vector2(min.x, min.y - .2f);
        return (corner1, corner2);
    }
}
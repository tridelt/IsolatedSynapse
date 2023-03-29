using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float movementSpeed = 3.0f;
    Vector2 movement = new Vector2();
    Rigidbody2D rb2d;
    Animator animator;
    SpriteRenderer spriteRenderer;


    enum CharStates
    {
        walkEast = 1,
        walkSouth = 2,
        walkWest = 3,
        walkNorth = 4,
        idle = 5
    }

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }
    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = Vector2.ClampMagnitude(movement, 1.0f);
        rb2d.velocity = movement * movementSpeed;
        UpdateState();
    }

    private void UpdateState()
    {
        CharStates state = CharStates.idle;
        if (movement.x > 0)
        {
            state = CharStates.walkEast;
        }
        else if (movement.x < 0)
        {
            state = CharStates.walkWest;
        }
        else if (movement.y > 0)
        {
            state = CharStates.walkNorth;
        }
        else if (movement.y < 0)
        {
            state = CharStates.walkSouth;
        }
        animator.SetInteger("AnimationState", (int)state);
        spriteRenderer.flipX = (state == CharStates.walkWest);
    }

}

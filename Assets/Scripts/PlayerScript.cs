using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    [SerializeField] float speed = 2f;

    [SerializeField] Rigidbody2D rb;

    [SerializeField] Animator animator;

    PlayerControls PlayerInput;

    Vector2 movement;

    enum PlayerState
    {
        Moving,
        Attacking
    }

    private void Awake()
    {
        PlayerInput = new PlayerControls();
        PlayerInput.Enable();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerState == PlayerState.Attacking)
        // Input
        movement = PlayerInput.Player.Move.ReadValue<Vector2>();

        // Animations
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        //Movement
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}

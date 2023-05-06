using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    GameObject ui_object;

    float current_health;

    [Header("Health")]
    [SerializeField]
    float max_health;

    [Header("Movement")]
    [SerializeField]
    float speed; // Movement speed for Selene

    [SerializeField]
    float dodge_force; // Dodge force for Selene

    [SerializeField]
    float dodge_duration; // Dodge duration for Selene

    [SerializeField]
    float dodge_cooldown; // Dodge force for Selene

    [SerializeField]
    Rigidbody2D rb; // Selene's Rigidbody2D component

    [SerializeField]
    Animator animator; // Selene's animator

    [SerializeField]
    Transform[] attack_points;

    [SerializeField]
    float attack_range;

    [SerializeField]
    LayerMask enemie_layers;

    [SerializeField]
    float damage_taken_shielded;

    [SerializeField]
    LayerMask rune_layer;

    [SerializeField]
    LayerMask blockade_layer;

    [SerializeField]
    GameObject hook;

    PlayerControls PlayerInput; // Input System for the player controls

    Vector2 movement; // Direction of the player movement

    bool attack_ready;
    bool shielded;
    bool dodge_ready;

    enum PlayerStates // Enum of the posible states of the player
    {
        Idle = 0,
        Moving = 1,
        Attacking = 2,
        Parrying = 3,
        Shielded = 4,
        Dodging = 5,
        UsingGadget = 6
    }

    enum PlayerDirections // Enum of the posible directions the player can face (is used to determine the idle or attack animation to use)
    {
        North = 0,
        South = 1,
        West = 2,
        East = 3
    }

    enum CurrentGadget
    {
        Hook = 0,
        Blaster = 1
    }

    PlayerStates player_state; // The current player state
    PlayerDirections player_direction; // The current direction the player is facing
    CurrentGadget current_gadget;

    private void Awake()
    {
        // Input System setups
        PlayerInput = new PlayerControls();
        PlayerInput.Enable();
        PlayerInput.Player.MeleeAttack.performed += Attack;
        PlayerInput.Player.Shield.started += Parry;
        PlayerInput.Player.Shield.performed += Shielded;
        PlayerInput.Player.Shield.canceled += Unshield;
        PlayerInput.Player.Dodge.performed += Dodge;
        PlayerInput.Player.Gadget.performed += Gadget;

        // Initial state and direction of the player
        player_state = PlayerStates.Idle;
        player_direction = PlayerDirections.South;

        //For the moment being we only have the hook, so we set it as default here
        current_gadget = CurrentGadget.Hook;
    }

    // Start is called before the first frame update
    void Start()
    {
        current_health = max_health;
        attack_ready = true;
        dodge_ready = true;
        shielded = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (
            player_state != PlayerStates.Attacking
            && player_state != PlayerStates.Parrying
            && player_state != PlayerStates.Shielded
            && player_state != PlayerStates.Dodging
            && player_state != PlayerStates.UsingGadget
        )
        {
            // Input
            movement = PlayerInput.Player.Move.ReadValue<Vector2>();

            if (movement != Vector2.zero)
                player_state = PlayerStates.Moving;
            else
                player_state = PlayerStates.Idle;

            // Animations
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }

        UpdatePlayerDirection();
    }

    private void FixedUpdate()
    {
        //Movement
        if (player_state == PlayerStates.Moving)
        {
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void UpdatePlayerDirection()
    {
        if (movement.x > 0.01)
        {
            if (movement.y > 0.01)
            {
                if (movement.x > movement.y)
                {
                    player_direction = PlayerDirections.East;
                }
                else
                {
                    player_direction = PlayerDirections.North;
                }
            }
            else if (movement.y < -0.01)
            {
                if (movement.x > Mathf.Abs(movement.y))
                {
                    player_direction = PlayerDirections.East;
                }
                else
                {
                    player_direction = PlayerDirections.South;
                }
            }
        }
        else if (movement.x < -0.01)
        {
            if (movement.y > 0.01)
            {
                if (Mathf.Abs(movement.x) > movement.y)
                {
                    player_direction = PlayerDirections.West;
                }
                else
                {
                    player_direction = PlayerDirections.North;
                }
            }
            else if (movement.y < -0.01)
            {
                if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y))
                {
                    player_direction = PlayerDirections.West;
                }
                else
                {
                    player_direction = PlayerDirections.South;
                }
            }
        }

        animator.SetFloat("Direction", (float)player_direction);
    }

    void Attack(InputAction.CallbackContext context)
    {
        if (attack_ready)
        {
            attack_ready = false;
            player_state = PlayerStates.Attacking;
            animator.SetTrigger("Attack");
            Collider2D[] enemies_hit = Physics2D.OverlapCircleAll(
                attack_points[(int)player_direction].position,
                attack_range,
                enemie_layers
            );
            foreach (Collider2D enemy in enemies_hit)
            {
                float attackDamage = 30;
                Enemy enemyController = null;
                SpecialEnemy specialEnemyController = null;
                Boss bossEnemyController = null;

                if (enemy.TryGetComponent(out enemyController))
                {
                    enemyController.TakeDamage(attackDamage);
                }
                if (enemy.TryGetComponent<SpecialEnemy>(out specialEnemyController))
                {
                    specialEnemyController.TakeDamage(attackDamage);
                }
                if (enemy.TryGetComponent(out bossEnemyController))
                {
                    bossEnemyController.TakeDamage(attackDamage);
                }
            }

            Collider2D[] runes_hit = Physics2D.OverlapCircleAll(
                attack_points[(int)player_direction].position,
                attack_range,
                rune_layer
            );
            foreach (Collider2D rune in runes_hit)
            {
                float attackDamage = 30;
                rune.GetComponent<Rune>().Triggered();
            }

            Collider2D[] blockades_hit = Physics2D.OverlapCircleAll(
                attack_points[(int)player_direction].position,
                attack_range,
                blockade_layer
            );
            foreach (Collider2D blockade in blockades_hit)
            {
                blockade.GetComponent<Blockade>().TakeDamage();
            }
            Invoke(nameof(AttackReset), 0.6f);
        }
    }

    void AttackReset()
    {
        attack_ready = true;
        player_state = PlayerStates.Idle;
    }

    void Parry(InputAction.CallbackContext context)
    {
        player_state = PlayerStates.Parrying;
        animator.SetTrigger("Parry");
    }

    void Shielded(InputAction.CallbackContext context)
    {
        shielded = true;
        player_state = PlayerStates.Shielded;
        animator.SetBool("Shielded", shielded);
    }

    void Unshield(InputAction.CallbackContext context)
    {
        shielded = false;
        player_state = PlayerStates.Idle;
        animator.SetBool("Shielded", shielded);
    }

    void Dodge(InputAction.CallbackContext context)
    {
        if (
            dodge_ready
            && player_state != PlayerStates.Attacking
            && player_state != PlayerStates.Parrying
            && movement != Vector2.zero
        )
        {
            dodge_ready = false;
            player_state = PlayerStates.Dodging;
            GetComponent<TrailRenderer>().emitting = true;
            animator.SetTrigger("Dodge");
            rb.velocity = movement * dodge_force;
            Invoke(nameof(DodgeStop), dodge_duration);
            Invoke(nameof(DodgeReset), dodge_cooldown);
        }
    }

    void DodgeStop()
    {
        rb.velocity = Vector2.zero;
        GetComponent<TrailRenderer>().emitting = false;
        player_state = PlayerStates.Idle;
    }

    void DodgeReset()
    {
        dodge_ready = true;
    }

    void Gadget(InputAction.CallbackContext context)
    {
        player_state = PlayerStates.UsingGadget;
        switch (current_gadget)
        {
            case CurrentGadget.Hook:
                Hook();
                break;
        }
    }

    void Hook()
    {
        hook.SetActive(true);
        hook.GetComponent<HookScript>().InitialDirection((int)player_direction);
    }

    public void HookEnded()
    {
        player_state = PlayerStates.Idle;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attack_points[(int)player_direction].position, attack_range);
    }

    public void TakeDamage(float damage)
    {
        if (player_state == PlayerStates.Shielded)
        {
            damage *= damage_taken_shielded;
        }
        current_health -= damage;

        if (current_health == 0)
        {
            animator.SetTrigger("Death");
            Invoke(nameof(HealthMax), 1f);
        }

        if (current_health < 0)
        {
            current_health = 0;
        }
        ui_object.GetComponent<PlayerUI>().UpdateHealth(current_health);
    }

    public void PorjectileImpacted(GameObject projectile)
    {
        if (player_state == PlayerStates.Parrying)
        {
            projectile.GetComponent<ProjectileScript>().ProjectileParried((int)player_direction);
        }
        else
        {
            TakeDamage(projectile.GetComponent<ProjectileScript>().damage);
            projectile.SetActive(false);
        }
    }

    void HealthMax()
    {
        current_health = 100;
        ui_object.GetComponent<PlayerUI>().UpdateHealth(current_health);
    }
}

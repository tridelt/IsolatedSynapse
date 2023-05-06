using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*


*/

public class Enemy : MonoBehaviour
{
    [SerializeField]
    float current_health;

    [SerializeField]
    float max_health;
    public Transform[] patrolPoints;
    public Transform player;
    public float moveSpeed;
    public int patrolDestination = 0;
    public float enemySpeed = 50.5f;
    public float meleeDistance = 0.3f;
    private Animator _anim;
    Vector3 _previousPosition;
    private bool _isFacingRight = false;
    private bool _hasAggro = false;
    public SpriteRenderer _spriteRenderer;
    private bool inMeleeRange = false;
    private Rigidbody2D rb;


    public bool isAlive
    {
        get { return current_health > 0; }
    }

    public Transform aggroCenter;

    void Start()
    {
        _anim = GetComponent<Animator>();
        _previousPosition = transform.position;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 movementDirection = Vector2.zero;
        movementDirection += (Vector2)(player.position - transform.position).normalized;
        rb.velocity = movementDirection.normalized * moveSpeed;
        // rb.velocity = movementDirection.normalized * Vector2.MoveTowards(
        //     transform.position,
        //     player.position,
        //     enemySpeed * Time.deltaTime
        // );
    }

    public void TakeDamage(float attackDamage)
    {
        if (!isAlive)
            return;

        current_health -= attackDamage;
        StartCoroutine(FlashRed());

        if (current_health <= 0)
        {
            _anim.SetBool("isDead", true);
            Invoke(nameof(EnemyDies), 5f);
        }
    }

    private void EnemyDies()
    {
        Destroy(gameObject, 1f);
    }

    private IEnumerator FlashRed()
    {
        yield return new WaitForSeconds(0.1f);

        // Change the color of the sprite to red
        _spriteRenderer.color = Color.red;

        // Wait for 0.1 seconds
        yield return new WaitForSeconds(0.3f);

        // Change the color of the sprite back to white
        _spriteRenderer.color = Color.white;
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(1.5f);
        while (inMeleeRange)
        {
            player.gameObject.GetComponent<PlayerScript>().TakeDamage(10f);
            yield return new WaitForSeconds(1.5f);
        }
    }
}

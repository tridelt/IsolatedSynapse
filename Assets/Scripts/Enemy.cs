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

    public bool isAlive
    {
        get { return current_health > 0; }
    }

    public Transform aggroCenter;

    //  void OnDrawGizmos()
    //  {
    //      print("Drawing Gizmos!");
    //      Gizmos.color = Color.red;
    //      Gizmos.DrawSphere(middlePatrolPoint, 1f);
    //  }

    void Start()
    {
        _anim = GetComponent<Animator>();
        _previousPosition = transform.position;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isAlive)
        {
            if (transform.position.y < player.position.y)
            {
                _spriteRenderer.sortingOrder = 2;
            }
            else
            {
                _spriteRenderer.sortingOrder = 0;
            }

            if (Vector2.Distance(player.position, aggroCenter.position) < 5f)
            {
                _hasAggro = true;
            }

            if (_hasAggro)
            {
                if (Vector2.Distance(transform.position, player.position) > meleeDistance)
                {
                    transform.position = Vector2.MoveTowards(
                        transform.position,
                        player.position,
                        enemySpeed * Time.deltaTime
                    );
                    _anim.SetBool("inMeleeRange", false);
                }
                else
                {
                    _anim.SetBool("inMeleeRange", true);
                }
            }
            else
            {
                transform.position = Vector2.MoveTowards(
                    transform.position,
                    patrolPoints[patrolDestination].position,
                    enemySpeed * Time.deltaTime
                );
                if (
                    Vector2.Distance(transform.position, patrolPoints[patrolDestination].position)
                    < 0.1f
                )
                {
                    patrolDestination = (patrolDestination + 1) % patrolPoints.Length;
                }
            }

            Vector3 movementDelta = transform.position - _previousPosition;
            _previousPosition = transform.position;

            if (movementDelta.x > 0 && !_isFacingRight)
            {
                transform.localScale = new Vector3(
                    -Mathf.Sign(transform.localScale.x),
                    transform.localScale.y,
                    1f
                );
                _isFacingRight = true;
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (movementDelta.x < 0 && _isFacingRight)
            {
                transform.localScale = new Vector3(
                    -Mathf.Sign(transform.localScale.x),
                    transform.localScale.y,
                    1f
                );
                _isFacingRight = false;
            }
        }
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
}

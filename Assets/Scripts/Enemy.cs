using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
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


    public Transform aggroCenter;
    // private Vector2 middlePatrolPoint;
    // private Rigidbody2D _body;


    // void Start()
    // {
    //     middlePatrolPoint = new Vector2(patrolPoints[0].transform.position.x - patrolPoints[1].transform.position.x,
    //         patrolPoints[0].transform.position.y - patrolPoints[1].transform.position.y);
    // }

    //  void OnDrawGizmos()
    //  {
    //      print("Drawing Gizmos!");
    //      Gizmos.color = Color.red;
    //      Gizmos.DrawSphere(middlePatrolPoint, 1f);
    //  }

    void Start()
    {
        _anim= GetComponent<Animator>();
        _previousPosition = transform.position;
    }


    void Update()
    {
        Debug.Log(_anim);

        /*
            every time reevaluate the state again
            --> puede ser {chasing, attacking, returning, patrolling}
            chasing, returning patrolling --> {moving, idle} maybe highly reusable, don't have to recode this everytime...
        */

        if (Vector2.Distance(player.position, aggroCenter.position) < 5f)
        {
            _hasAggro = true;
        }

        if (_hasAggro)
        {
            // Debug.Log("chasing enemey");
            // chase mode
            // check if in melee range
            if (Vector2.Distance(transform.position, player.position) > meleeDistance)
            {
                transform.position =
                    Vector2.MoveTowards(transform.position, player.position, enemySpeed * Time.deltaTime);
                _anim.SetBool("inMeleeRange", false);
            }
            else
            {
                Debug.Log(_anim);
                _anim.SetBool("inMeleeRange", true);
            }
        }
        else
        {
            // Debug.Log("patrolling...");
            // patrol
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[patrolDestination].position,
                enemySpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, patrolPoints[patrolDestination].position) < 0.1f)
            {
                patrolDestination = (patrolDestination + 1) % patrolPoints.Length;
            }
        }

        Vector3 movementDelta = transform.position - _previousPosition;
        _previousPosition = transform.position;

        if (movementDelta.x > 0 && !_isFacingRight)
        {
            transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, 1f);
            _isFacingRight = true;
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (movementDelta.x < 0 && _isFacingRight)
        {
            // ... flip the player.
            transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, 1f);
            _isFacingRight = false;
        }
    }
}
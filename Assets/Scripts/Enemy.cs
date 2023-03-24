using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform[] patrolPoints;
    public Transform player;
    public float moveSpeed;
    public int patrolDestination = 0;
    public float enemySpeed = 2.5f;
    public float meleeDistance = 0.3f;

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


    void Update()
    {
        /*
            every time reevaluate the state again
            --> puede ser {chasing, attacking, returning, patrolling}
            chasing, returning patrolling --> {moving, idle} maybe highly reusable, don't have to recode this everytime...
        */

        if (Vector2.Distance(player.position, aggroCenter.position) < 5f)
        {
            // Debug.Log("chasing enemey");
            // chase mode
            // check if in melee range
            if (Vector2.Distance(transform.position, player.position) > meleeDistance)
            {
                transform.position =
                    Vector2.MoveTowards(transform.position, player.position, enemySpeed * Time.deltaTime);
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


        // else {
        //     if(patrolDestination == 0) {
        //         // patrol
        //         transform.position = Vector2.MoveTowards(transform.position, patrolPoints[0].position, speed * Time.deltaTime);
        //         if(Vector2.Distance(transform.position, patrolPoints[0].position) < 0.1f) {
        //             patrolDestination = 1;
        //         }
        //     } else if(patrolDestination == 1) {
        //         // patrol
        //         transform.position = Vector2.MoveTowards(transform.position, patrolPoints[1].position, speed * Time.deltaTime);
        //         if(Vector2.Distance(transform.position, patrolPoints[1].position) < 0.1f) {
        //             patrolDestination = 0;
        //         }
        //     }
        // }


        // if(patrolDestination == 0) {
        //     // patrol
        //     transform.position = Vector2.MoveTowards(transform.position, patrolPoints[0].position, speed * Time.deltaTime);
        //     if(Vector2.Distance(transform.position, patrolPoints[0].position) < 0.1f) {
        //         patrolDestination = 1;
        //     }
        // } else if(patrolDestination == 1) {
        //     // patrol
        //     transform.position = Vector2.MoveTowards(transform.position, patrolPoints[1].position, speed * Time.deltaTime);
        //     if(Vector2.Distance(transform.position, patrolPoints[1].position) < 0.1f) {
        //         patrolDestination = 0;
        //     }
        // }

        // if(Vector2.Distance(transform.position, player.transform.position) < 5f)
        // {
        //     Debug.Log("CLOSE");
        //     // chase
        //     // transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        // }
        // else
        // {
        //        Debug.Log("FAR AWAY");
        //     // patrol
        //     // transform.position = Vector2.MoveTowards(transform.position, new Vector2(0, 0), speed * Time.deltaTime);
        // }


        // float deltaX = Input.GetAxis("Horizontal") * speed;
        // Vector2 movement = new Vector2(_body.velocity.x, -speed);
        // _body.velocity = movement;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HookScript : MonoBehaviour
{
    public string[] tags;

    public float speed, returnSpeed;
    public float range;

    float rotation;

    bool returning;

    enum FixedRotations
    {
        North = 0,
        South = -180,
        West = -270,
        East = -90
    }

    //Private variables

    [SerializeField] Transform player;

    [SerializeField] LineRenderer line;

    private void Start()
    {
        returning = false;
    }

    private void Update()
    {
        line.SetPosition(0, player.position);
        line.SetPosition(1, transform.position);

        var dist = Vector2.Distance(transform.position, player.position);

        if (returning)
        {
            transform.position += transform.up * Time.deltaTime * returnSpeed;
        }
        else
        {
            if (dist > range)
            {
                ChangeRotation(rotation + 180);
                returning = true;
            }

            transform.position += transform.up * Time.deltaTime * speed;
        }
    }

    private void ChangeRotation(float newRotation)
    {
        CancelInvoke();
        rotation = newRotation;
        transform.rotation = Quaternion.Euler(0, 0, rotation);
    }

    public void InitialDirection(int playerDirection)
    {
        transform.position = player.position;
        switch (playerDirection)
        {
            case 0:
                ChangeRotation((float)FixedRotations.North);
                break;
            case 1:
                ChangeRotation((float)FixedRotations.South);
                break;
            case 2:
                ChangeRotation((float)FixedRotations.West);
                break;
            case 3:
                ChangeRotation((float)FixedRotations.East);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Projectile" && !returning)
        {
            int direction = 0;
            switch (rotation)
            {
                case 0:
                    direction = 1;
                    break;
                case -90:
                    direction = 2;
                    break;
                case -270:
                    direction = 3;
                    break;
            }
            collision.GetComponent<ProjectileScript>().ProjectileParried(direction);
            returning = true;
            ChangeRotation(rotation + 180);
        }
        else if (collision.tag == "PuzzleBox" && !returning)
        {
            Vector2 direction = new Vector2(0,1);
            switch (rotation)
            {
                case 0:
                    direction = new Vector2(0, -1);
                    break;
                case -90:
                    direction = new Vector2(-1, 0);
                    break;
                case -270:
                    direction = new Vector2(1, 0);
                    break;
            }
            collision.GetComponent<BoxScript>().Hooked(direction);
            returning = true;
            ChangeRotation(rotation + 180);
        }
        else if (collision.tag == "Player" && returning)
        {
            gameObject.SetActive(false);
            player.gameObject.GetComponent<PlayerScript>().HookEnded();
        }
        else if (collision.tag != "Player" && collision.tag != "BrokenBox")
        {
            returning = true;
            ChangeRotation(rotation + 180);
        }
    }

    private void OnDisable()
    {
        returning = false;
    }

}

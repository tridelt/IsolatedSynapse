using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    [SerializeField] float movementSpeed;

    [SerializeField] float damage;

    [SerializeField] float disableTime;

    enum FixedRotations
    {
        North = 0,
        South = -180,
        West = -250,
        East = -90
    }

    float rotation;

    bool parried;

    // Start is called before the first frame update
    void Start()
    {
        //Invoke(nameof(Disable), disableTime);
    }

    private void Awake()
    {
        //Invoke(nameof(Disable), disableTime);
        //parried = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * Time.deltaTime * movementSpeed;
    }

    public void ProjectileParried(int playerDirection)
    {

        parried = true;

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

    public void ChangeRotation(float newRotation)
    {
        rotation = newRotation;
        transform.rotation = Quaternion.Euler(0,0,rotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !parried)
        {
            collision.GetComponent<PlayerScript>().TakeDamage(damage);
        }
        else if(collision.tag == "Enemy" && parried)
        {
            collision.GetComponent<PlayerScript>().TakeDamage(damage);
        }

        Disable();
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Invoke(nameof(Disable), disableTime);
        parried = false;
    }

}

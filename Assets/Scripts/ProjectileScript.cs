using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    [SerializeField]
    float movementSpeed;

    [SerializeField] public float damage;

    [SerializeField]
    float disableTime;

    enum FixedRotations
    {
        North = 0,
        South = -180,
        West = -270,
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
        CancelInvoke();
        Invoke(nameof(Disable), disableTime);
        rotation = newRotation;
        transform.rotation = Quaternion.Euler(0, 0, rotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !parried)
        {
            collision.GetComponent<PlayerScript>().PorjectileImpacted(gameObject);
        }
        else if (collision.tag == "Enemy" && parried)
        {
            Debug.Log(damage);
            Enemy enemyController = null;
            SpecialEnemy specialEnemyController = null;
            Boss bossEnemyController = null;

            if (collision.TryGetComponent(out enemyController))
            {
                enemyController.TakeDamage(damage);
            }
            if (collision.TryGetComponent(out specialEnemyController))
            {
                specialEnemyController.TakeDamage(damage);
            }
            if (collision.TryGetComponent(out bossEnemyController))
            {
                bossEnemyController.TakeDamage(damage);
            }
            Disable();
        }
        else if (collision.tag == "Enemy" && collision.tag == "Projectile")
        {
            Disable();
        }
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        parried = false;
    }
}

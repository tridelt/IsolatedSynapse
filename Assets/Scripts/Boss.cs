using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private Animator anim;
    private Transform player_pos;
    private float timeBtwShots;
    private bool sleeping = true;
    private float health = 100;
    private bool isDead = false;
    private int flipDirection = -1;

    public Transform instancePoint;
    public GameObject linearBulletPrefab;
    public GameObject directionBulletPrefab;

    private void Start()
    {
        anim = GetComponent<Animator>();
        player_pos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void Wake()
    {
        sleeping = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (sleeping)
        {
            return;
        }

        if (Vector2.Distance(transform.position, player_pos.position) > 8)
        {
            anim.SetBool("isWalking", true);

            transform.position = Vector2.MoveTowards(
                transform.position,
                player_pos.position,
                1f * Time.deltaTime
            );
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

        if (player_pos.position.x < transform.position.x)
        {
            flipDirection = 1;
        }
        else
        {
            flipDirection = -1;
        }

        transform.localScale = new Vector2(flipDirection, 1);

        timeBtwShots += Time.deltaTime;
        if (timeBtwShots >= 2f)
        {
            int random = Random.Range(0, 2);
            if (random == 0)
            {
                StartCoroutine(SimpleShot());
            }
            else
            {
                StartCoroutine(SpecialShot());
            }
            timeBtwShots = 0;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0 && !isDead)
        {
            isDead = true;
            anim.SetBool("isDead", true);
            Destroy(gameObject, 1.5f);
        }
        else
        {
            anim.SetBool("isHurt", true);
        }
    }

    IEnumerator SimpleShot()
    {
        anim.SetTrigger("Attack 1");
        yield return new WaitForSeconds(1.0f);
        Instantiate(linearBulletPrefab, instancePoint.position, Quaternion.identity);
    }

    IEnumerator SpecialShot()
    {
        anim.SetTrigger("Attack 2");
        yield return new WaitForSeconds(1.0f);
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.1f);
            float angle = -60;
            for (int j = 0; j < 8; j++)
            {
                DirectionProjectile bulletScript =
                    directionBulletPrefab?.GetComponent<DirectionProjectile>();
                bulletScript.SetValues(2, angle, -flipDirection);
                Instantiate(directionBulletPrefab, instancePoint.position, Quaternion.identity);
                angle += 20;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public AudioClip dragonDeath;
    public AudioClip fireball;
    AudioSource audioSource;

    private void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        player_pos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void Wake()
    {
        sleeping = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (sleeping || isDead)
        {
            return;
        }

        if (Vector2.Distance(transform.position, player_pos.position) > 6)
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
        if (timeBtwShots >= 4f)
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
            anim.SetBool("isDead", true);
            isDead = true;
            StartCoroutine(DisplayFinalDialog());
        }
        else
        {
            anim.SetBool("isHurt", true);
            StartCoroutine(ResetIsHurtTriggered());
        }
    }

    IEnumerator SimpleShot()
    {
        anim.SetTrigger("Attack 1");
        yield return new WaitForSeconds(1.5f);
        audioSource.clip = fireball;
        audioSource.Play();
        Instantiate(linearBulletPrefab, instancePoint.position, Quaternion.identity);
    }

    IEnumerator SpecialShot()
    {
        anim.SetTrigger("Attack 2");
        yield return new WaitForSeconds(1.5f);
        audioSource.clip = fireball;
        audioSource.Play();
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

    IEnumerator ResetIsHurtTriggered()
    {
        yield return new WaitForSeconds(0.1f);
        anim.SetBool("isHurt", false);
    }

    IEnumerator DisplayFinalDialog()
    {
        yield return new WaitForSeconds(6f);
        Actor[] actors = new Actor[1];
        Message[] messages = new Message[2];
        actors[0] = new Actor();
        actors[0].name = "Selene";
        messages[0] = new Message();
        messages[0].actorId = 0;
        messages[0].message = "Parece que lo he logrado...";
        messages[1] = new Message();
        messages[1].actorId = 0;
        messages[1].message = "Ahora podre volver a casa...";
        FindObjectOfType<DialogManager>().OpenDialogue(messages, actors);
    }
}

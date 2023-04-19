using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour
{
    private Animator anim;

    private Transform player_pos;
    public Transform instancePoint;
    public GameObject bulletPrefab;
    private float timeBtwShots;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Choose simple shot or special shot randomly
        timeBtwShots += Time.deltaTime;

        if (timeBtwShots >= 2f)
        {
            int random = Random.Range(0, 2);
            Debug.Log(random);
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

    IEnumerator SimpleShot()
    {
        anim.SetTrigger("Attack 1");
        yield return new WaitForSeconds(1.0f);
        Instantiate(bulletPrefab, instancePoint.position, Quaternion.identity);
    }

    IEnumerator SpecialShot()
    {
        anim.SetTrigger("Attack 2");
        yield return new WaitForSeconds(1.0f);
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.1f); // Esperar medio segundo entre cada disparo
            Instantiate(bulletPrefab, instancePoint.position, Quaternion.identity);
        }
    }
}

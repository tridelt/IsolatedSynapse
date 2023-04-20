using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEnemy : MonoBehaviour
{
    public GameObject ammoPrefab;
    static List<GameObject> ammoPool;
    public int poolSize;

    [SerializeField]
    float rotationAugment;

    [SerializeField]
    GameObject attackZoneInner,
        attackZoneOuter;

    [SerializeField]
    Transform player;

    float current_heatlh;

    [SerializeField]
    float max_health;

    bool weakened,
        dead;

    Animator anim;

    enum EnemyStates
    {
        Idle = 0,
        Attacking = 1
    }

    EnemyStates state;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        weakened = false;
        dead = false;
        current_heatlh = max_health;
    }

    private void Awake()
    {
        if (ammoPool == null)
        {
            ammoPool = new List<GameObject>();
        }
        for (int i = 0; i < poolSize; i++)
        {
            GameObject ammoObject = Instantiate(ammoPrefab);
            ammoObject.SetActive(false);
            ammoPool.Add(ammoObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (current_heatlh <= 0 && !dead)
        {
            dead = true;
            anim.SetTrigger("Death");
            Invoke(nameof(Disable), 2.25f);
        }

        GetComponent<SpriteRenderer>().flipX = player.position.x > transform.position.x;

        //For testing only
        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    PlayerEntered(player.gameObject);
        //}
        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    Weakened();
        //}
        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    PlayerExited();
        //}
        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    TakeDamage(100);
        //}
    }

    private void Attack()
    {
        if (state == EnemyStates.Attacking && !dead)
        {
            if (weakened)
            {
                StartCoroutine(WeakenedAttack());
            }
            else
            {
                StartCoroutine(BaseAttack());
            }

            Invoke(nameof(Attack), 12f);
        }
    }

    private IEnumerator BaseAttack()
    {
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(1f);
        float rotation = 0;
        GameObject projectile;
        GameObject projectile2;
        GameObject projectile3;
        GameObject projectile4;
        for (int i = 0; i < 25 && !(dead || weakened); i++)
        {
            projectile = SpawnAmmo(transform.position);
            projectile.GetComponent<ProjectileScript>().ChangeRotation(rotation);

            projectile2 = SpawnAmmo(transform.position);
            projectile2.GetComponent<ProjectileScript>().ChangeRotation(-90 + rotation);

            projectile3 = SpawnAmmo(transform.position);
            projectile3.GetComponent<ProjectileScript>().ChangeRotation(-180 + rotation);

            projectile4 = SpawnAmmo(transform.position);
            projectile4.GetComponent<ProjectileScript>().ChangeRotation(-250 + rotation);

            rotation += rotationAugment * Time.deltaTime;

            yield return new WaitForSeconds(0.2f);
        }
        anim.SetTrigger("AttackEnd");
    }

    private IEnumerator WeakenedAttack()
    {
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(0.75f);
        float rotation = AngleV2(player.position - transform.position);
        GameObject projectile = SpawnAmmo(transform.position);
        projectile.GetComponent<ProjectileScript>().ChangeRotation(rotation);
    }

    public void Weakened()
    {
        weakened = true;
        anim.SetLayerWeight(0, 0);
        anim.SetLayerWeight(1, 1);
    }

    public void TakeDamage(float damage)
    {
        current_heatlh -= damage;
    }

    private GameObject SpawnAmmo(Vector3 location)
    {
        foreach (GameObject ammo in ammoPool)
        {
            if (ammo.activeSelf == false)
            {
                ammo.SetActive(true);
                ammo.transform.position = location;
                return ammo;
            }
        }
        return null;
    }

    private void OnDestroy()
    {
        ammoPool = null;
    }

    public void PlayerEntered(GameObject playerEnter)
    {
        player = playerEnter.transform;
        state = EnemyStates.Attacking;
        Attack();
    }

    public void PlayerExited()
    {
        state = EnemyStates.Idle;
    }

    public static float AngleV2(Vector2 vector2)
    {
        if (vector2.x < 0)
        {
            return 360 - (Mathf.Atan2(-vector2.x, vector2.y) * Mathf.Rad2Deg * -1);
        }
        else
        {
            return Mathf.Atan2(-vector2.x, vector2.y) * Mathf.Rad2Deg;
        }
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}

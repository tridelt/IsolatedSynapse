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

    bool weakened, dead, attack_in_process;

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
        attack_in_process = false;
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
            GetComponents<AudioSource>()[2].Play();
            dead = true;
            anim.SetTrigger("Death");
            Invoke(nameof(Disable), 2.75f);
        }

        if(!dead) GetComponent<SpriteRenderer>().flipX = player.position.x > transform.position.x;

        //For testing only
        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    PlayerEntered(player.gameObject);
        //}
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Weakened();
        }
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
        if (state == EnemyStates.Attacking && !dead && !attack_in_process)
        {
            if (weakened)
            {
                GetComponents<AudioSource>()[1].Play();
                StartCoroutine(WeakenedAttack());
            }
            else
            {
                GetComponents<AudioSource>()[0].Play();
                StartCoroutine(BaseAttack());
            }

            Invoke(nameof(Attack), 12f);
        }
    }

    private IEnumerator BaseAttack()
    {
        attack_in_process = true;
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
        attack_in_process = false;
    }

    private IEnumerator WeakenedAttack()
    {
        attack_in_process = true;
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(0.75f);
        float rotation = AngleV2(player.position - transform.position);
        GameObject projectile = SpawnAmmo(transform.position);
        projectile.GetComponent<ProjectileScript>().ChangeRotation(rotation);
        attack_in_process = false;
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
        FlashRed();
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
        if (!attack_in_process)
        {
            player = playerEnter.transform;
            state = EnemyStates.Attacking;
            Attack();
        }
    }

    public void PlayerExited()
    {
        state = EnemyStates.Idle;
        //CancelInvoke();
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

    private IEnumerator FlashRed()
    {
        yield return new WaitForSeconds(0.1f);
        SpriteRenderer _spriteRenderer = GetComponent<SpriteRenderer>();

        // Change the color of the sprite to red
        _spriteRenderer.color = Color.red;

        // Wait for 0.1 seconds
        yield return new WaitForSeconds(0.3f);

        // Change the color of the sprite back to white
        _spriteRenderer.color = Color.white;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blockade : MonoBehaviour
{
    private Animator _anim;
    public SpriteRenderer _spriteRenderer;
    [SerializeField] float health = 3;

    public bool isAlive
    {
        get { return health > 0; }
    }

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void EnemyDies()
    {
        Destroy(gameObject, 1f);
    }

    public void TakeDamage()
    {
        Debug.Log("2322333blockate hit");

        if (!isAlive) return;

        health -= 1;
        StartCoroutine(DamageFlashing());

        if (!isAlive)
        {
            Debug.Log("it is now dead");
            _anim.SetBool("destroyed", true);
            Invoke(nameof(EnemyDies), 0f);
        }
    }

    private IEnumerator DamageFlashing()
    {

        yield return new WaitForSeconds(0.1f);

        // Change the color of the sprite to red
        _spriteRenderer.color = Color.white;

        // Wait for 0.1 seconds
        yield return new WaitForSeconds(0.1f);

        // Change the color of the sprite back to white
        _spriteRenderer.color = Color.grey;
        
        // Wait for 0.1 seconds
        yield return new WaitForSeconds(0.1f);

        // Change the color of the sprite back to white
        _spriteRenderer.color = Color.white;
    }
}
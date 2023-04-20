using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    [SerializeField]
    public GameObject enemy;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            enemy.GetComponent<Boss>().Wake();
        }
    }

    private void OnDrawGizmos()
    {
        if (enemy != null)
        {
            BoxCollider2D bc = GetComponent<BoxCollider2D>();
            Gizmos.DrawWireCube(bc.bounds.center, bc.bounds.size);
            Gizmos.DrawLine(bc.bounds.center, enemy.gameObject.transform.position);
        }
    }
}

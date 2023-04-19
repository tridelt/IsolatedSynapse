using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackZoneSpecialEnemy : MonoBehaviour
{

    [SerializeField] bool inner;
    [SerializeField] GameObject parent;

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (inner && collision.tag == "Player")
        {
            parent.GetComponent<SpecialEnemy>().PlayerEntered();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!inner && collision.tag == "Player")
        {
            parent.GetComponent<SpecialEnemy>().PlayerExited();
        }
    }
}

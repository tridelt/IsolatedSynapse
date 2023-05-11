using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablePotion : MonoBehaviour
{
    public float amplitude = 0.2f;
    private Vector3 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = initialPosition;
        newPosition.y += Mathf.Sin(Time.time) * amplitude;
        transform.position = newPosition;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerScript>().HealthMax();
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectOnTimer : MonoBehaviour
{

    [SerializeField] float TimeTillDestroy;

    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(DestroySelf), TimeTillDestroy);
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}

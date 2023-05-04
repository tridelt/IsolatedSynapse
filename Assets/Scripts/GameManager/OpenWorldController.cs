using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWorldController : MonoBehaviour
{

    [SerializeField] Transform player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadOpenWorldStatus(GameObject pos, bool key)
    {
        player.position = pos.transform.position;
    }
}

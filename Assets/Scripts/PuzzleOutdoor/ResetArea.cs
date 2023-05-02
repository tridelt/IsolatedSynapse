using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResetArea : MonoBehaviour
{
    public UnityEvent onStateChanged; // Event to be triggered when the state is changed

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        onStateChanged.Invoke();
    }
}
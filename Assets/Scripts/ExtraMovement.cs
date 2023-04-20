using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraMovement : MonoBehaviour
{
    float timeToChange;

    // Update is called once per frame
    void Update()
    {
        timeToChange += Time.deltaTime;
        if (timeToChange >= 5f)
        {
            transform.Translate(Vector2.left * Time.deltaTime);
            transform.localScale = new Vector2(-1, 1);
        }
        else
        {
            transform.Translate(Vector2.right * Time.deltaTime);
            transform.localScale = new Vector2(1, 1);
        }

        if (timeToChange >= 10f)
        {
            timeToChange = 0;
        }
    }
}

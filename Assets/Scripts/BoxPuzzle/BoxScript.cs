using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    [SerializeField] GameObject PuzzleManager;

    public void Hooked(Vector2 direction)
    {
        if (PuzzleManager.GetComponent<BoxPuzzleManager>().CanMoveBox(transform.position, direction))
        {
            transform.position += (Vector3)direction;
        }
    }
}

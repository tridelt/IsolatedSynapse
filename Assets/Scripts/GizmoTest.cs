using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoTest : MonoBehaviour
{
    void OnDrawGizmos()
    {
        print("Drawing Gizmos!");
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(Vector3.zero, 1f);
    }
}

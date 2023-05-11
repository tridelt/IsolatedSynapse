using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseController : MonoBehaviour
{
    [SerializeField] GameObject Hook;

    public void LoadHouseStatus(bool hook)
    {
        Hook.SetActive(!hook);
    }
}

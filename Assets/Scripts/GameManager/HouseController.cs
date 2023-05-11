using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HouseController : MonoBehaviour
{
    [SerializeField] GameObject Hook;
    [SerializeField] TextMeshProUGUI key_text;

    public void LoadHouseStatus(bool hook, bool key)
    {
        Hook.SetActive(!hook);
        if (key) key_text.text = "1";
    }
}

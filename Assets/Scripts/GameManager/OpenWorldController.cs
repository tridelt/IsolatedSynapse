using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OpenWorldController : MonoBehaviour
{

    [SerializeField] Transform player;
    [SerializeField] TextMeshProUGUI key_text;
    [SerializeField] GameObject DungeonDoor, DungeonBlockade1, DungeonBlockade2, DungeonBlockade3, Puzzle;

    public void LoadOpenWorldStatus(Vector3 pos, bool key, bool hook, bool puzzle)
    {
        player.position = pos;

        DungeonDoor.SetActive(key && hook);
        DungeonBlockade1.SetActive(!key && !hook);
        DungeonBlockade2.SetActive(!key && hook);
        DungeonBlockade3.SetActive(key && !hook);

        if (puzzle) StartCoroutine(SetOutdorPuzzleSolved(key));

        if (key) key_text.text = "1";
    }

    public void UpdateBlockades(bool key, bool hook)
    {
        DungeonDoor.SetActive(key && hook);
        DungeonBlockade1.SetActive(!key && !hook);
        DungeonBlockade2.SetActive(!key && hook);
        DungeonBlockade3.SetActive(key && !hook);

        if (key) key_text.text = "1";
    }

    private IEnumerator SetOutdorPuzzleSolved(bool key)
    {
        yield return new WaitForSeconds(1f);
        Puzzle.GetComponent<Manager>().SetPuzzleSolved(key);
    }
}

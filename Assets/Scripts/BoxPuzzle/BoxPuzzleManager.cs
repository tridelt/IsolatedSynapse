using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BoxPuzzleManager : MonoBehaviour
{

    [SerializeField] GameObject[] box_list;

    [SerializeField] GameObject player;

    [SerializeField] Tilemap tilemap;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CanMoveBox(Vector3 position, Vector2 direction)
    {
        Vector3Int new_position = tilemap.WorldToCell(position + (Vector3)direction);
        bool can_move = tilemap.WorldToCell(player.transform.position) != new_position;
        for(int i = 0; i < box_list.Length && can_move; i++)
        {
            can_move = (tilemap.WorldToCell(box_list[i].transform.position) != new_position);
        }
        return can_move;
    }
}

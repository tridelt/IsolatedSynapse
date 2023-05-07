using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

    [SerializeField] GameObject open_world_spawn_point;
    [SerializeField] float max_health;
    float current_heatlh;


    // Bools for the open world
    bool hook_collected, key_obtained, outdoor_puzzle_completed;

    // Bools for the dungeon
    bool first_barrier_broken, second_barrier_broken, puzzle_one_completed, puzzle_two_completed;

    // Bools for the house
    bool chest_opened;

    // Start is called before the first frame update
    void Start()
    {
        current_heatlh = 100;
        key_obtained = outdoor_puzzle_completed = first_barrier_broken = second_barrier_broken = puzzle_one_completed = puzzle_two_completed = chest_opened = false;
    }

    public void ChangeOpenWorldSpawnPoint(GameObject new_spawn_point)
    {
        open_world_spawn_point = new_spawn_point;
    }

    public void LoadPlayerStatus(out float health, out bool hook)
    {
        health = current_heatlh;
        hook = hook_collected;
    }

    public void UpdatePlayerStatus(float health, bool hook)
    {
        current_heatlh = health;
        hook_collected = hook;
    }

    public void LoadSceneStatus()
    {
        System.String current_scene = SceneManager.GetActiveScene().name;
        GameObject SceneController = GameObject.Find("SceneController");
        switch (current_scene)
        {
            case "OpenWorld":
                SceneController.GetComponent<OpenWorldController>().LoadOpenWorldStatus(open_world_spawn_point, key_obtained);
                break;
        }
    }

    private void UpdateOpenWorld()
    {

    }
}

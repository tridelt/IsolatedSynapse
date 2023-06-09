using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    Vector3 open_world_spawn_point;

    [SerializeField]
    float max_health;
    float current_heatlh = 100;

    // Bools for the open world and player
    bool hook_collected,
        key_obtained,
        outdoor_puzzle_completed;

    // Start is called before the first frame update
    void Start()
    {
        current_heatlh = max_health;
        key_obtained = outdoor_puzzle_completed = false;
    }

    public void ChangeOpenWorldSpawnPoint(GameObject new_spawn_point)
    {
        open_world_spawn_point = new_spawn_point.transform.position;
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
                SceneController
                    .GetComponent<OpenWorldController>()
                    .LoadOpenWorldStatus(
                        open_world_spawn_point,
                        key_obtained,
                        hook_collected,
                        outdoor_puzzle_completed
                    );
                break;
            case "House":
                SceneController.GetComponent<HouseController>().LoadHouseStatus(hook_collected, key_obtained);
                break;
        }
    }

    public void OpenWorldPuzzleCompleted()
    {
        outdoor_puzzle_completed = true;
    }

    public void KeyObtained()
    {
        key_obtained = true;
        GameObject SceneController = GameObject.Find("SceneController");
        SceneController.GetComponent<OpenWorldController>().UpdateBlockades(key_obtained, hook_collected);
    }

}

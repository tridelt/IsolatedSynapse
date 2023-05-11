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
    float current_heatlh;

    // Bools for the open world and player
    bool hook_collected,
        key_obtained,
        outdoor_puzzle_completed;

    // Start is called before the first frame update
    void Start()
    {
        current_heatlh = 100;
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

        if (hook_collected)
        {
            DisplayDialogHook();
        }
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
                SceneController.GetComponent<HouseController>().LoadHouseStatus(hook_collected);
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
        SceneController
            .GetComponent<OpenWorldController>()
            .UpdateBlockades(key_obtained, hook_collected);
    }

    void DisplayDialogKey()
    {
        Actor[] actors = new Actor[1];
        Message[] messages = new Message[2];
        actors[0] = new Actor();
        actors[0].name = "Selene";
        messages[0] = new Message();
        messages[0].actorId = 0;
        messages[0].message = "Creo que a esta llave la podre utilizar en algun lugar...";

        FindObjectOfType<DialogManager>().OpenDialogue(messages, actors);
    }

    void DisplayDialogHook()
    {
        Actor[] actors = new Actor[1];
        Message[] messages = new Message[2];
        actors[0] = new Actor();
        actors[0].name = "Selene";
        messages[0] = new Message();
        messages[0].actorId = 0;
        messages[0].message =
            "Que interesante esta herramienta, creo que me seria de utilidad mas adelante...";

        FindObjectOfType<DialogManager>().OpenDialogue(messages, actors);
    }
}

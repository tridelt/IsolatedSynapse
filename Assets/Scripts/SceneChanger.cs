using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField]
    System.String scene;

    [SerializeField]
    GameObject SpawnPoint;

    GameObject GameManager;

    private void Start()
    {
        GameManager = GameObject.Find("GameManager");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (SceneManager.GetActiveScene().name == "OpenWorld")
            {
                GameManager.GetComponent<GameManager>().ChangeOpenWorldSpawnPoint(SpawnPoint);
            }
            Invoke(nameof(LoadNextScene), 0.5f);
        }
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(scene);
    }
}

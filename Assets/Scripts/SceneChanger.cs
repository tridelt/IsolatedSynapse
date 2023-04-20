using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField]
    System.String scene;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Invoke(nameof(LoadDungeon), 0.5f);
    }

    void LoadDungeon()
    {
        SceneManager.LoadScene(scene);
    }
}

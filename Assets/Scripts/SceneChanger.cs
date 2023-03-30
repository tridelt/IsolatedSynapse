using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] System.String scene;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    Debug.Log("BBBBBBBBBBBBBBBBBBb");
    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}
    private void OnTriggerEnter2D(Collider2D other)
    {
        Invoke(nameof(LoadDungeon), 0.5f);
    }

    void LoadDungeon()
    {
        SceneManager.LoadScene(scene);
    }
}

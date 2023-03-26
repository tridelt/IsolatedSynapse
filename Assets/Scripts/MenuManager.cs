using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{

    [SerializeField] Button startButton; //Reference to the start button
    [SerializeField] private

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        
        //startButton.Select(); //AutoSelects the start button to enable navigation with the controller
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level1 Name"); //Loads the first level (Name has to be passed as a String)
    }

    public void ExitGame()
    {
        Application.Quit(); //Exits the game similar to pressing Alt+F4
    }
}

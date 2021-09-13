using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{

    public string tutorialLevel;
    public string firstLevel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Loads the level indicated by the given string.
    public void loadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    //Exits the game.
    public void exitGame()
    {
        Application.Quit();
    }
}

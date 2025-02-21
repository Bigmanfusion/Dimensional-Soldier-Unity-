using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuscript : MonoBehaviour
{
    //The header of a functionm NEVER has a semicolon in it
    //This function takes no arguments/parameters but you must still include the parentheses!
    public void ExitGame()
    {
        Debug.Log("Quitted");
        Application.Quit();


    } //Every { must have a } to match it

    //We added a string argument to this one, so it can be reused for other scenes
    public void LoadLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}

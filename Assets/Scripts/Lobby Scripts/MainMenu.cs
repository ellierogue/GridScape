/*#-----------Wizards of the Codes Documentation-----------#
Contributors: Trevor Pechous
Purpose: Handle menu behavior
Game Objects Associated: All objects in listed scenes
Files Associated: MainMenu.Unity Lobby.Unity
Source: Self-Written
#----------------------#
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // On click loads the user into the lobby creation scene
    }

    public void QuitGame()
    {
        Debug.Log("Quit"); // Exits the game
        Application.Quit();
    }
    public void GoBack()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1); // Returns the user to the previous scene
    }
    public void setFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen; //Handles fullscreen toggle 
    }
}

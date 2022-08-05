using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// script to handle Menu Buttons 
/// </summary>
public class MenuManager : MonoBehaviour
{
    /// <summary>
    /// Restarts/loads the Game Level
    /// </summary>
    public void LoadLevel(int i)
    {
        SceneManager.LoadScene(i);

    }
    

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
    public void Exit()
    {
        Application.Quit();
    }
}

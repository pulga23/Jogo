using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainMenu : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) //press enter to return to menu
        {
            SceneManager.LoadScene("MainMenu"); //load menu scene
        }
       
        if (Input.GetKeyDown(KeyCode.Escape)) //press escape to quit game
        {
            Application.Quit(); 
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadTheGame : MonoBehaviour
{
    [SerializeField]
    GameObject menuPanel;
    [SerializeField]
    GameObject creditsPanel;

    private void Start()
    {
        menuPanel.SetActive(true); //show menu panel
        creditsPanel.SetActive(false); //hide credits panel
    }
   
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) //press enter to start the Game
        {
            SceneManager.LoadScene("GamePlay"); //load gameplay scene
        }
        else if (Input.GetKeyDown(KeyCode.Escape)) //press escape to quit game
        {
            Application.Quit();
        }
        else if(Input.GetKeyDown(KeyCode.Space)) //press space to go to the credits
        {
            menuPanel.SetActive(false); //hides menu panel
            creditsPanel.SetActive(true); //show credits panel
        }
        else if(Input.GetKeyDown(KeyCode.Backspace)) //returns to menu panel, if not on credits panel, nothing changes
        {
            menuPanel.SetActive(true); //show menu panel
            creditsPanel.SetActive(false); //hide credits panel
        }
    }
    /*Load game scene 
    public void LoadGame()
    {
        SceneManager.LoadScene("GamePlay");
    }
    
    //Quit the game
    public void QuitTheGame()
    {
        Application.Quit();
    }*/
}

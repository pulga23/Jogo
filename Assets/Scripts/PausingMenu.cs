using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausingMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private bool isPaused;

    public void FS()
    {
        Debug.Log("fullscreen");
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void Update()
    {
         if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }

         if (isPaused)
        {
            ActivateMenu();
        }

         else
        {
            DeactivateMenu();
        }
    } 

    void ActivateMenu()
    {
        Time.timeScale = 0;
        AudioListener.volume = 1f;
        pauseMenu.SetActive(true);

    }

    public void DeactivateMenu()
    {
        Time.timeScale = 1;
        AudioListener.volume = 0.2f;
        pauseMenu.SetActive(false);

    }

    public void ReiniciarPausa() 
    {
    SceneManager.LoadScene("MainMenu");
    }

   public void SairPausa()
   {
    Application.Quit();
    }
    

    
}

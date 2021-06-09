using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] GameObject optionsMenu;
    [SerializeField] GameObject credits;


    public void FS()
    {
        Debug.Log("fullscreen");
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void AudioOff()
    {
        Debug.Log("audiooff");
        AudioListener.pause = true;
    }

    public void AudioOn()
    {
        Debug.Log("audioon");
        AudioListener.volume = 1f;
    }

    private void Start()
    {
        Debug.Log("start");
        credits.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void Reniciar(int game)
    {
        Debug.Log("reiniciar");
        SceneManager.LoadScene(game);
    }

    public void Creditos()
    {
        credits.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void Sair()
    {
        Debug.Log("sair");
        Application.Quit();
    }
}


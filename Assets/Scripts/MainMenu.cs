using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


//https://moodle.esmad.ipp.pt/mod/page/view.php?id=7669 

public class MainMenu : MonoBehaviour
{
    //dar a conhecer as "paginas" de creditos e instruçoes
    [SerializeField] GameObject optionsMenu;
    [SerializeField] GameObject credits;
    [SerializeField] GameObject instructions;


    public void FS() //botao ecra inteiro
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void Iniciar(int gamePlay) //iniciar jogo atraves da cena nomeada pela RitaMarques
    {
        SceneManager.LoadScene("CutScene");
    }

    public void Options() //no main (opçoes), desaparece creditos e instruçoes
    {
        optionsMenu.SetActive(true); 
        instructions.SetActive(false);
        credits.SetActive(false);
    }

    public void Instruçoes() //passar para as isntruçoes, desaparece main e creditos
    {
        instructions.SetActive(true); 
        optionsMenu.SetActive(false);
        credits.SetActive(false);
    }

    public void Creditos() //passar para os creditos, desaparece main e insrtruçoes
    {
        credits.SetActive(true); 
        optionsMenu.SetActive(false);
        instructions.SetActive(false);
    }

    public void Sair() //sair do jogo
    {
        Application.Quit(); 
    }
}


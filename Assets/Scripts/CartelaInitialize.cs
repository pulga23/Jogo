using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CartelaInitialize : MonoBehaviour
{
    VideoPlayer video;

    void Awake()
    {
        //Carrega video
        video = GetComponent<VideoPlayer>();
        video.Play();
        //Verifica video
        video.loopPointReached += CheckOver;
    }


    void CheckOver(UnityEngine.Video.VideoPlayer vp)
    {
        //Carrega cena qd video chega fim
        SceneManager.LoadScene("MainMenu");
    }
}

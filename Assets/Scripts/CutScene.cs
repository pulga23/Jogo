using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutScene : MonoBehaviour
{
    float time = 3f;

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if(time<0)//shot over load the game scene
        {
            SceneManager.LoadScene("GamePlay");
        }
    }
}

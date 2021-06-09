using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    //criaçao de colisão para surgir o som feedback de coletaveis e portais
    void OnTriggerEnter(Collider colisao)
    {
        GetComponent<AudioSource>().Play(); //o som é disparado quando entra na zona da esfera
    }
    void OnTriggerExit(Collider colisao)
    {
        GetComponent<AudioSource>().Stop(); //o som pára quando sai da zona da esfera
    }
}

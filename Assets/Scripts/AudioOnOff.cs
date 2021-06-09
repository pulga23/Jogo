using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioOnOff : MonoBehaviour
{

    [SerializeField] private GameObject sound; //chamar o botão

    private SpriteRenderer soundOn; //dar a conhecer o sprite do audio

    private int soundState; //dar a conhecer os possíveis estados: ou está on ou está off; ou seja: ou está 1 ou está 0

    [SerializeField] private Sprite[] switchSprites; //um sprite vai ser mudado para outro

    private Image switchImage;

    private bool muted = false; //porque fica a 0 quando desligamos o som, i.e., quando clicamos no icone

    // Start is called before the first frame update
    void Start()
    {

        AudioListener.pause = muted; //jogo inicia a saber que pausa significa retirar o som
        
        soundOn = sound.GetComponent<SpriteRenderer>();
        soundState = 1; // audio está ligado no início

        switchImage = GetComponent<Button>().image;
        switchImage.sprite = switchSprites[soundState]; //dependendo do estado do audio (on ou off) o sprite será alterado

        gameObject.GetComponent<Button>().onClick.AddListener(TurnOnAndOff); //adicionar componente de observação daquilo que está a acontecer quando o botão é carregado 
    }

    private void TurnOnAndOff()
    {
        soundOn.color = new Color(1f, 1f, soundState, 1f); //usa-se o color para chegar ao sprite renderer
        soundState = 1 - soundState;
        switchImage.sprite = switchSprites[soundState]; //o sprite muda conforme o 1 ou o 0, i.e., o estado  
    }

    public void OnButtonPress() //conforme o clique no botao/icone do audio, quando muted é true, i.e., fica em pausa; quando muted é false, o som é reproduzido
    {
        if(muted == false)
        {
            muted = true;
            AudioListener.pause = true;
        }
        else
        {
            muted = false;
            AudioListener.pause = false;
        }

   
    }

  
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXScreaming : MonoBehaviour
{
    //https://www.youtube.com/watch?v=JnbDxG04i7c
    //https://docs.unity3d.com/ScriptReference/GameObject-tag.html 


    //variables for identifying the audios i'll use

    public AudioSource AmandaSeringv1;
    public AudioSource AmandaSeringv2;
    public AudioSource DoctorKidnap;
    public AudioSource ManBurn;
    public AudioSource HandsBlood;

    private void OnTriggerEnter(Collider other)
    {
        //for playing the correspondet sound when character passes certain places (connecting to cubes on unity scene)

        if (tag == "AmandaV1")
        {
            AmandaSeringv1.Play();
        }

        if (tag == "AmandaV2")
        {
            AmandaSeringv2.Play();
        }

        if (tag == "DocKidnap")
        {
            DoctorKidnap.Play();
        }

        if (tag == "ManBurning")
        {
            ManBurn.Play();
        }

        if (tag == "HandsBloody")
        {
            HandsBlood.Play();
        }

        
        
    }
}

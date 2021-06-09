using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bips : MonoBehaviour
{
    AudioSource bipAudio;
    public void ActivateBips()
    {
        //bips life machine from john almost dying, activate after 5min
        bipAudio = GetComponent<AudioSource>();
        bipAudio.PlayDelayed(10f);
        //https://www.youtube.com/watch?v=V6DX1XmSpeA; https://answers.unity.com/questions/290222/play-sound-on-fire-c.html
    }
}

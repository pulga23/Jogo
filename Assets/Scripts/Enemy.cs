using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent enemy; //robot as navmesh agent
    Transform target; //player's position is the target
    Vector3 enemyVelocity;

    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform; //FPS controller has the tag player so we get that transform

        source = GetComponent<AudioSource>();
        //https://www.studytonight.com/game-development-in-2D/audio-in-unity 


        //AudioSource audio = gameObject.AddComponent<AudioSource>();
        //audio.PlayOneShot((AudioClip)Resources.Load("tricyclePuppet"));
    }

    // Update is called once per frame
    void Update()
    {
        enemy.destination = target.position; // enemy destination is player's position

        //we want the sound of saw puppet trycicle to play
        source.Play();
    }

    //stop ans restart methods adapted from https://forum.unity.com/threads/solved-how-can-i-stop-navmesh-agent-sliding-in-unity5.332233/ and https://docs.unity3d.com/Manual/StaticObjects.html
    public void StopEnemy()
    {
        enemy.isStopped = true; //stop navmesh agent movement

        //trycicle sound of enemy pauses when enemy stops moving
        source.Pause();
    }
    public void RestartEnemy()
    {
        enemy.isStopped = false; //navmesh agent movement resumes in current path

        //trycicle sound of enemy continues when enemy resumes moving
        source.Play();
    }
}

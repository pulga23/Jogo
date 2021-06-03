using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent enemy; //robot as navmesh agent
    Transform target; //player's position is the target
    Vector3 enemyVelocity;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform; //FPS controller has the tag player so we get that transform
    }

    // Update is called once per frame
    void Update()
    {
        enemy.destination = target.position; // enemy destination is player's position
    }

    //stop ans restart methods adapted from https://forum.unity.com/threads/solved-how-can-i-stop-navmesh-agent-sliding-in-unity5.332233/ and https://docs.unity3d.com/Manual/StaticObjects.html
    public void StopEnemy()
    {
        enemy.isStopped = true; //stop navmesh agent movement
    }
    public void RestartEnemy()
    {
        enemy.isStopped = false; //navmesh agent movement resumes in current path
    }
}

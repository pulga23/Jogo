using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Antidote : MonoBehaviour
{
    [SerializeField]
    GameObject poisonSphere;
    [SerializeField]
    float poisonTimer = 3f;
    float countTime = 0f;
    bool poisonOn = true;
        
    // Update is called once per frame
    void Update()
    {
        if(!poisonOn)
        {
            CountPoisonTime(); //call method to count the time
        }
    }
    //player shoots the sphere
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ammo")) //if it is hit with ammo let's turn it off
        {
            poisonOn = false; //turn poison off
            poisonSphere.SetActive(false); //hide game object
            GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>().StopEnemy(); //callm ethod stop enemy in the enemy script to stop the navmesh agent
        }
    }
    
    //count time poison is off
    private void CountPoisonTime()
    {
        if(countTime<poisonTimer) //time passed less than time set for the poison to be off
        {
            countTime +=Time.deltaTime; //counting time passed
        }
        if(countTime>=poisonTimer) //time has passed
        {
            countTime = 0f; //restart variable
            poisonOn = true; // turn poison on 
            poisonSphere.SetActive(true); //show game object 
            GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>().RestartEnemy(); //after time has passed restar navmesh agent by calling the restart method in their script
        }
    }
}

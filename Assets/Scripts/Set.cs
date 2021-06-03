using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Set : MonoBehaviour
{
    [SerializeField]
    GameObject labyrinthNormal;  //
    [SerializeField]
    GameObject player;
    [SerializeField]
    private FirstPersonController fpc;      
    Vector3 auxPosition;
    Vector3 playerPosition;
    
    [SerializeField]
    private float timePortalSet = 10f; //time before each portal deactivates 
    private float timePortalPassed = 0f; //time the portal has been on
    private bool countPortalTime = false; //if a portal has been activated yet
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {      
        TimeThePortal();
    }
    //method to rotate the set in 90degrees in the z axis, called in the player script when player triggers tag portalZaxis
    public void ActivatePortalZAxis()
    {
        playerPosition = player.transform.position; //get current FPS position
        auxPosition = labyrinthNormal.transform.position; //get labyrinth position
        auxPosition.x = playerPosition.x; //value of x of the labyritnth is equal to the value of x of the player
        labyrinthNormal.transform.position = auxPosition; //atribute new values to labyrithn transform 
        labyrinthNormal.transform.rotation = Quaternion.Euler(0f, 0f, 90f); //rotate labyrinth in 90degrees in the Z axis
        Physics.gravity = new Vector3(0f, -4f, 0f); //change gravity 
        countPortalTime = true; //start counting time;
    }
    //method to rotate the player 180 degrees, called in the player script when player triggers tag portal180
    public void ActivatePortal180()
    {
        //auxPosition = labyrinthNormal.transform.position; //get labyrinth position
        //auxPosition.y = 3f;
        //labyrinthNormal.transform.position = auxPosition;
        fpc.m_MouseLook.m_CameraTargetRot = Quaternion.Euler(0f, 0f, 180f);
        countPortalTime = true; //start counting time;
    }
    //method to deactivate the portal and change back to normal
    private void DeactivatePortal()
    {
        labyrinthNormal.transform.rotation = Quaternion.Euler(0f, 0f, 0f); //labyrinth rotation 0 in all axis again
        fpc.m_MouseLook.m_CameraTargetRot = Quaternion.Euler(0f, 0f, 0f); //fps rotation normal
        Physics.gravity = new Vector3(0, -9.81f, 0); //make gravity  normal
    }
    //method to count the time the portal are active
    private void TimeThePortal()
    {
        if (countPortalTime) //only counts the time if a portal has been activated
        {
            if (timePortalPassed < timePortalSet) //portal still active
            {
                timePortalPassed += Time.deltaTime; //add time that is passing
            }
            else if (timePortalPassed >= timePortalSet)//time's up let's deactivate the portal
            {
                timePortalPassed = 0f; //restart the timer
                countPortalTime = false; // stop counting time
                DeactivatePortal(); //call method to deactivate the portal
            }
        }
    }
}

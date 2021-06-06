using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour
{
    private float maxForce=50f; //maximum value for the debris force
    private float minForce=5f; //minimun value for the debris force
    private float force;
    private Vector3 direction;

    private int lives = 3;//times the player has to hit the debris so it's destroyed
    // Start is called before the first frame update
    void Start()
    {
        direction = Random.insideUnitCircle; //random direction adapted from https://docs.unity3d.com/ScriptReference/Random-insideUnitSphere.html
        force = Random.Range(minForce, maxForce); //random force between min and max
        GetComponent<Rigidbody>().AddForce(direction * force); //gives the rigid body the foce in the direction calculated before
    }

    //Debris hits something
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ammo")) //if it has tag ammo
        {
            lives--; //remove a life to the debris
            if(lives==0) //if it's zero
            {
                Destroy(gameObject); //destroy debris
            }
        }
    }
}

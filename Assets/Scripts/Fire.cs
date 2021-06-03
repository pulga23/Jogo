using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField]
    float force = 100f; //force to shoot the ammo

    private Camera fpsCam; // first person Camera
        
    // Start is called before the first frame update
    void Start()
    {
       fpsCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>(); //get cam propreties
        
       GetComponent<Rigidbody>().AddForce(fpsCam.transform.forward * force); //give the shot a force in the direction the camera is facing - adapted from https://learn.unity.com/tutorial/let-s-try-shooting-with-raycasts#5c7f8528edbc2a002053b468
    }
    //when ammo collides with something
    private void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject); //destroys the ammo after a collision
       
    }
}

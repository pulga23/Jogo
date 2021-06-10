using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //LIVES
    [SerializeField]
    public int lives = 3; //how many lives the player has
    [SerializeField]
    private Text livesText; //print number of lives on screen
   
    //WEAPON AND FIRING
    [SerializeField]
    private GameObject pickUpPrompt; //prompt the player to press key to pick up object
    [SerializeField]
    GameObject aimPlayer; //player aim that shows on screen
    [SerializeField]
    GameObject playerSlingshot; // game object of the slingshot. to show and hide
    [SerializeField]
    GameObject elasticLong; //game object of the slingshot's elastic to show when aiming
    [SerializeField]
    GameObject elasticShort; //game object of the slingshot's elastic to show when reloading
    [SerializeField]
    private int weaponPart = 0; //variable to control the number of parts of the sling shot the player has 
    [SerializeField]
    GameObject playerAmmo; //what the player fires
    [SerializeField]
    private float fireRate = 1f; //rate thst the player can fire at, time to reload
    private float timeBetweenShots;
    [SerializeField]
    private bool playerHAsWeapon = false; //check if the player has all the parts and can use the weapon       
    private bool canFire = true; //player can fire if it's been more than fireRate seconds since last shot
    Transform slingshot; //where the shot comes from
    Vector3 auxFire;
    
    //FLASHLIGHT
    [SerializeField]
    GameObject flashlight; //player's flashlight
    bool flashlightOn = false;

    //WINNING
    bool hasAntidote = false; //variable to control if the player has the antidote yet
    [SerializeField]
    int antidotesNeeded =1;
    int antidotesOwned = 0;

    //FOG
    private bool fog = false; //variable to control if the fog is on or off
    private float fogDensity = 0.1f; // control fog density
    private Color fogColor = new Color(0.5f, 0.5f, 0.5f, 1f); //variable color to keep the fog color for when we want to turn fog on adapted from https://docs.unity3d.com/ScriptReference/Color.html


    private void Start()
    {
        pickUpPrompt.gameObject.SetActive(false); //hide pick up prompt
        aimPlayer.gameObject.SetActive(false); //hide aim  
        flashlight.gameObject.SetActive(false); //turn off flashlight
        GameObject.FindGameObjectWithTag("DoorExit").SetActive(true);//show exit game object so player can't escape the labyrinth without the antidote
        playerSlingshot.gameObject.SetActive(false); //hide slingshot at the start of the game
        elasticLong.gameObject.SetActive(false);//hide elastics at the start of the game
        elasticShort.gameObject.SetActive(false);//hide elastics at the start of the game
        
        timeBetweenShots = fireRate;

        //RenderSettings.fogDensity = fogDensity; //set fog density to 0.1f
        //RenderSettings.fogColor = fogColor; //change color to fog color
        //RenderSettings.fog = true; //enable fog in the lighting settings 
        //fog = true; //fog is on
    }
    
    private void Update()
    {
        livesText.text = lives.ToString(); //print number of lives to screen
        
        Flashlight(); //using the flashlight
        
        //slingshot complete and ready to use 
        if (playerHAsWeapon == true) 
        {
            UseSlingShot(); //calls method that controls the slingshot
        }

        //call metehod when the player has collected the antidote 
        if(hasAntidote)
        {
            GameObject.FindGameObjectWithTag("DoorExit").SetActive(false);//hide exit game object so plater can escape the labyrinth
        }
       
    }
    //method to control the use of the slingshot, player aims using the right button on the mouse and while holding it can fire using the left button
    private void UseSlingShot()
    {
        //while right button is down aim is shown and player can fire
        if (Input.GetMouseButton(1))
        {
            aimPlayer.gameObject.SetActive(true); //show player aim
            playerSlingshot.gameObject.SetActive(true); //show slingshot
            if (canFire)
            {
                elasticLong.gameObject.SetActive(true); //if player is aiming and can fire elastic charged is showing
                elasticShort.gameObject.SetActive(false); //if player is aiming and can fire elastic short is hidden
            }
            if (!canFire)
            {
                elasticLong.gameObject.SetActive(false); //if player is aiming and can't fire elastic charged is hidden
                elasticShort.gameObject.SetActive(true); //if player is aiming and can't fire elastic short is showing

            }
            //when left button is pressed fire
            if (Input.GetMouseButtonDown(0) && canFire == true) //check if left button is pressed and if it has pass enough time since last shot 
            {
                canFire = false; //so timer begins

                slingshot = transform;
                auxFire = slingshot.position;
                auxFire.y++;
                slingshot.position = auxFire; //increse by one the height of the shot
                //SSOOUUUNNNDD shot fires - gets sound clip used in unity scene trough name of clip and audio source component
                AudioSource audio = gameObject.AddComponent<AudioSource>();
                audio.PlayOneShot((AudioClip)Resources.Load("disparo"));
                //https://answers.unity.com/questions/290222/play-sound-on-fire-c.html

                Instantiate(playerAmmo, slingshot.position, slingshot.rotation); //instatiate player shots
            }
        }
        //when right button released hide aim and slingshot
        if (Input.GetMouseButtonUp(1))
        {
            aimPlayer.gameObject.SetActive(false);//hide aim when right button is released
            playerSlingshot.gameObject.SetActive(false); //hide player slingshot
            elasticLong.gameObject.SetActive(false); //hide elastic
            elasticShort.gameObject.SetActive(false); //hide elastic
        }
            
        //counts the time between shots
        if (!canFire)
        {
            timeBetweenShots -= Time.deltaTime; //countdown
            if(timeBetweenShots <=0) //when it hits zero
            {
                canFire = true; //set to true and it can fire again
                timeBetweenShots = fireRate; //restart variable
            }
        }
    }
    //runs when the player touches something
    private void OnTriggerEnter(Collider other)
    {
        //player touches a poisonous plant
        if (other.CompareTag("PoisonousPlant"))
        {
            lives--; //remove a live to the player
            if (lives <= 0)
            {
                GameObject.FindGameObjectWithTag("Set").GetComponent<Geral>().GameOver(); //start Game Over method when player has no lives left
            }
        }
        //player collects a health kit which means an extra live
        if(other.CompareTag("HealthKit"))
        {
            lives++; //adds a live to the player
            Destroy(other.gameObject); //destroy health kit
        }
        //player touches a portal
        //if(other.CompareTag("PortalZAxis")) //portal that rotates 90 degrees in the Z axis
        //{
          //  GameObject.FindGameObjectWithTag("Set").GetComponent<Set>().ActivatePortalZAxis(); //call method on Set script that activates the portal
        //}
        if(other.CompareTag("Portal180")) //portal that rotates the player cam 180 degrees
        {
            GameObject.FindGameObjectWithTag("Set").GetComponent<Set>().ActivatePortal180(); //call method on Set script that activates the portal
        }
        //player touches debris
        if (other.CompareTag("Debris"))
        {
            lives--; //remove a live to the player
            if (lives <= 0)
            {
                GameObject.FindGameObjectWithTag("Set").GetComponent<Geral>().GameOver(); //start Game Over method when player has no lives left
            }
        }
        //player enters robot poisonous zone
        if(other.CompareTag("PoisonRobot"))
        {
            lives--;
            if (lives <= 0)
            {
                GameObject.FindGameObjectWithTag("Set").GetComponent<Geral>().GameOver(); //start Game Over method when player has no lives left
            }

        }
        //player reaches the exit zone
        if(other.CompareTag("Exit"))
        {
            //call game win scene 
            SceneManager.LoadScene("Victory");
        }
        //player touches fog trigger- adapted from https://docs.unity3d.com/ScriptReference/RenderSettings.html
        if (other.CompareTag("Fog"))
        {
            if (GameObject.FindGameObjectWithTag("Set").GetComponent<Geral>().gasReleased == false) //fog turns on if the poison has not been released, if gasreleased variable is false
            { 
                if (!fog) //fog is off let's turn it on 
                {
                    RenderSettings.fogDensity = fogDensity; //set fog density to 0.1f
                    RenderSettings.fogColor = fogColor; //change color to fog color
                    RenderSettings.fog = true; //enable fog in the lighting settings 
                    fog = true; //fog is on
                }
                else if (fog) //fog is on let's turn it off
                {
                    RenderSettings.fog = false; //disable fog in the lighting settings 
                    fog = false; //fog is off
                }
            }
        }
        //player falls jumping between room and labyrinth
        if(other.CompareTag("Abyss"))
        {
            GameObject.FindGameObjectWithTag("Set").GetComponent<Geral>().GameOver(); //player dies immediately, start Game Over method 
        }
    }
    //player collects items
    private void OnTriggerStay(Collider other)
    {
        //player is touching a weapon part and collects it. THERE ARE ONLY 3 PARTS IN THE LABIRYNTH AND THE PLAYER HAS TO CATCH THEM ALL  
        if (other.CompareTag("WeaponPart"))
        {
            pickUpPrompt.gameObject.SetActive(true); //print pick up prompt
            //when C is pressed
            if (Input.GetKeyDown(KeyCode.C))
            {
                Destroy(other.gameObject); //destroy weapon part game object
                weaponPart++; //add weapon part
                pickUpPrompt.gameObject.SetActive(false); //hide pick up prompt
            }
        }
        //player has all the parts and can use the weapon
        if(weaponPart==3) 
        {
            playerHAsWeapon = true;
        }
        //player collects antidote
        if(other.CompareTag("Antidote"))
        {
            pickUpPrompt.gameObject.SetActive(true); //print pick up prompt
            if (Input.GetKeyDown(KeyCode.C))
            {
                Destroy(other.gameObject); //destroy antidote game object- this destorys the poison shpere too because controlling the turning on and off is the antidote script that is in the antidote game object
                antidotesOwned++; //add antidote
                pickUpPrompt.gameObject.SetActive(false); //hide pick up prompt
                if(antidotesOwned==antidotesNeeded)
                {
                    hasAntidote = true;
                }
            }

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("WeaponPart"))
        {
            pickUpPrompt.gameObject.SetActive(false); //hides pick up prompt even if player doesn't collect weapon part
        }
        if(other.CompareTag("Antidote"))
        {
            pickUpPrompt.gameObject.SetActive(false); //hides pick up prompt even if player doesn't collect antidote

        }
    }
    //method to control the flashlight
    private void Flashlight()
    {
        if (Input.GetMouseButtonDown(2) || Input.GetKeyDown(KeyCode.F)) //flashlight button is pressed
        {
            if (flashlightOn) //flashlight is on, turn it off
            {
                flashlight.gameObject.SetActive(false); //turns flashlight off
                flashlightOn = false;
            }
            else if (!flashlightOn) //flashlight off, turn it on
            {
                flashlight.gameObject.SetActive(true);
                flashlightOn = true;
            }
        }
    }
   
}

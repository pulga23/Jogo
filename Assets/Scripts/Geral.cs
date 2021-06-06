using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Geral : MonoBehaviour
{
    [SerializeField]
    private float timeLeft = 300f; //time the character has to get through the game
    [SerializeField]
    private Text timeText; //time the player has left to show on screen
    private bool timeRunning=true; //variable to make the time count stop when we want or when time is zero
    private float minutes;
    private float seconds;

    [SerializeField]
    private float releaseGasTime = 30f; //when player has this time left gas is released to kill them  
    public bool gasReleased = false; // variable to know if the gas has already been released, public to acess it in player script 

    [SerializeField]
    private GameObject pauseScreen; // game object to stpre the pause screen
    private bool gamePaused = false; // variable to know if the game is paused

    bool fog = false; //variable to control if the fog is on or off
    Color poisonColor = new Color(0.7f, 0.5f, 0.3f, 1f); //variable colour to keep the color for the poison to be when released
    float poisonTime = 0f; //variable to count the tima that has passed since poison has been realesed

    //[SerializeField]
    //GameObject debris;
    //[SerializeField]
    //Transform[] debrisCoordinates = new Transform[5];// create empty game objects as coordinates and place in array
    //int debrisAmount = 5; //number of debris
    
    [SerializeField]
    Transform[] coordinatesWeaponParts = new Transform[5];
    [SerializeField]
    private GameObject weaponPart;
    int firstPosition;
    int secondPosition;
    int thirdposition;
    int partsInGame = 0;
    private void Start()
    {
        pauseScreen.gameObject.SetActive(false); //hides the pause screen 
        Time.timeScale = 1f; //start time

        firstPosition = Random.Range(0, 5);
        Instantiate(weaponPart, coordinatesWeaponParts[firstPosition].position, Quaternion.identity);//instatiate first weapon part
        partsInGame++; //one part in game 
        InstatiateWeaponPart();
    }

    void Update()
    {
        CountAndPrintTime(); //calling method to cont and print to the scrren the time
        if (Input.GetKeyDown(KeyCode.P)) //pause  or unpause the game
        {
            if (gamePaused == false) //game is running, let's pause it
            {
                PauseGame(); //call the method to pause the game
            }
            else if (gamePaused == true) //game is paused let's return
            {
                ReturnGame(); //call the method to return to the game
            }
        }
        if (gamePaused == true) //if game is paused player can return to the menu or quit the game
        {
            if (Input.GetKeyDown(KeyCode.Return)) //press enter to return to menu
            {
                SceneManager.LoadScene("MainMenu"); //load menu scene
            }
            if (Input.GetKeyDown(KeyCode.Escape)) //press escape to quit game
            {
                Application.Quit();
            }
        }
        if (gasReleased) RealeasePoison(); //call method when the variable is true
    }

    //method to instatiate weapon parts
    private void InstatiateWeaponPart()
    {
        while(partsInGame < 3)//runs while less than 3 parts are in the game
        {
            if(partsInGame==1) //only one part in game, instatiate the second
            {
                secondPosition = Random.Range(0, 5);
                if(secondPosition!=firstPosition) //second position diferent from first so we can instatiate
                {
                    Instantiate(weaponPart, coordinatesWeaponParts[secondPosition].position, Quaternion.identity);//instatiate second weapon part
                    partsInGame++; //two weapon parts in game
                }
            }
            if(partsInGame==2) //two parts in game, instatiate the thrid
            {
                thirdposition = Random.Range(0, 5);
                if((thirdposition != firstPosition) && (thirdposition != secondPosition)) //3rd diferent from 1st and 2nd so instatiate
                {
                    Instantiate(weaponPart, coordinatesWeaponParts[thirdposition].position, Quaternion.identity);//instatiate third weapon part
                    partsInGame++; //three weapon parts in game, all parts in game exit while
                }
            }
        }
    }
    //method to count and print to unity the time remaining
    //adapted from https://gamedevbeginner.com/how-to-make-countdown-timer-in-unity-minutes-seconds/
    private void CountAndPrintTime()
    {
        //count the time
        if(timeRunning == true) //check if time is still running
        {
            if(timeLeft < releaseGasTime && gasReleased == false) //time when gas is realeased
            {
                RealeasePoison();
            }
            if (timeLeft > 0f) //check if there is time left
            {
                timeLeft = timeLeft - Time.deltaTime; //contdown the time the player has left
            }
            if(timeLeft <= 0f)
            {
                timeRunning = false; //bool variable to make it only run once
                timeLeft = 0f;
                GameOver(); //call method that runs when player dies
            }
        }
        //convert time to format minutes:seconds and print
        minutes = Mathf.FloorToInt(timeLeft / 60); //multiply per 60 to get the minutes
        seconds = Mathf.FloorToInt(timeLeft % 60); //modular operation by 60 to get the seconds
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds); //print the time to the game screen in the format 00:00   
    }
    
    //method to run when the player dies
    public void GameOver() 
    { 
        timeRunning = false; //stop counting time
        timeLeft = 0f; //set time to 0
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().lives = 0; //set lives to 0
        SceneManager.LoadScene("GameOver"); //load game over scene
    }

    //method to pause the game
    private void PauseGame()
    {
        gamePaused = true; //game is now paused, so the variable is true
        pauseScreen.gameObject.SetActive(true); //shows pause screen
        Time.timeScale = 0f; //"stops" the game
    }

    //method to return to the game
    private void ReturnGame()
    {
        gamePaused = false; // game is not paused, so the variable is false
        pauseScreen.gameObject.SetActive(false); // hides pause screen
        Time.timeScale = 1f; //time starts agaisn
    }
        
    //method to release the poison - - adapted from https://docs.unity3d.com/ScriptReference/RenderSettings.html
    private void RealeasePoison()
    {
        gasReleased = true; //method start being called in update
        if (poisonTime == 0) //first poison setting
        {
            RenderSettings.fogDensity = 0.05f; //frist poison density
            RenderSettings.fogColor = poisonColor; //change color to poison color
            RenderSettings.fog = true; //enable fog in the lighting settings 
            fog = true; //fog is enable in the settings
        }
        else if (poisonTime >= 10f && poisonTime <=11) //second poison setting
        {
            RenderSettings.fogDensity = 0.1f; //second poison density, other settings remain the same 
        }
        else if (poisonTime >= 15f && poisonTime <= 16) //third poison setting
        {
            RenderSettings.fogDensity = 0.15f; //third poison density, other settings remain the same 
        }
        else if (poisonTime >= 20f && poisonTime <= 21) //fourth and last poison setting
        {
            RenderSettings.fogDensity = 0.15f; //fourth poison density, other settings remain the same 
        }
        poisonTime += Time.deltaTime; //count the time that is passing
    }
}

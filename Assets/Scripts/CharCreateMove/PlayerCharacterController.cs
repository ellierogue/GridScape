/*#-----------Wizards of the Codes Documentation-----------#
Contributors: Stephanie Simpson
Purpose: This code block handles everything associated with player character objects. It allows players to instantiate characters, handles movement, deletion, change model, etc. 
Also calls PlayerNetwork script and 
Game Objects Associated: SampleScene, PlayerController, Characters (where instantiated characters are stored), Spawn Character Btn, Move Character Btn, Delete Character Btn.
Files Associated: PlayerNetwork.cs, Testing.cs,
Source: Self-Written
#----------------------#
*/
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerCharacterController : MonoBehaviourPun
{
    //Stores the possible character models (in same order as the Character Selection Screen since it uses that index)
    //This is set on the PlayerController game object
    public GameObject[] playerPrefabs;

    //stores the instantiated character
    private GameObject playerCharacter;

    //sets whether the player has selected the spawn character or move character buttons
    private bool isMoving = false;
    private bool isSpawning = false;
     
    //Uses the Testing class to get grid points
    private Testing grid;

    

    // Start is called before the first frame update
    void Start()
    {
        //Finds the created Testing gameobject that is in the hierarchy
        GameObject gridGameObj = GameObject.Find("Testing");
        
        //Sets the grid to the Testing class so it can use the classes
        grid = gridGameObj.GetComponent<Testing>();
        
    }

    private void Update()
    {
        //Whether the user has selected the Spawn Character Button
        if (isSpawning)
        {
            //Whether the user has selected a grid space to place their character
            if (Input.GetMouseButtonDown(0))
            {
                //Sends the location of the grid space that the player selected
                SpawnPlayer(Mouse3D.GetMouseWorldPosition());
                
                //Sets spawning to false so characters aren't continually instantiated
                isSpawning = false;
            }
        }

        //Whether the player selected the Move Character Button
        else if (isMoving)
        {
            //Whether the player has selected a grid space to move the character to
            if (Input.GetMouseButtonDown(0))
            {
               //Sends the grid location that the player selected
                MoveCharacter(Mouse3D.GetMouseWorldPosition());
                
                //Turns off moving so the character won't move if another grid location is selected
                isMoving = false;
                
            }
        }
    }

    //Called from the Spawn Character Button Gameobject
    public void InitSpawn()
    {
        //Used to see if the character is actually spawning
        Debug.Log("Player began spawning");
        
        //Sets spawning to true which is then checked in the Update function
        isSpawning = true;
    }

    //Called from the Move Character Button when selected
    public void InitMoving()
    {
        isMoving = true;

    }

    //Called from the Update function once it has been determined that isSpawning is true
    public void SpawnPlayer(Vector3 position)
    {
        //Finds the grid position that the player selected
        Vector3 spawnPointOnGrid = grid.getPositionOnGrid(position);
        
        //Finds the character model that the player selected in the Character Selection Scene. Uses an int for the index
        //The character models are stored in the PlayerController gameobject
        GameObject playerToSpawn = playerPrefabs[PlayerPrefs.GetInt("characteravatar")];

        //Instantiates the character
        //Gets the name of the character model, the point on the grid that the player selected and doesn't rotate the model
        playerCharacter = PhotonNetwork.Instantiate(playerToSpawn.name, spawnPointOnGrid, Quaternion.identity);
        
        //Allows the PlayerNetworked class to be used
        PlayerNetworked currPlayerNetworked = playerCharacter.GetComponent<PlayerNetworked>();

        //Uses the PlayerNetworked class and setParent function to set the instantiated player model in the Characters gameobject
        currPlayerNetworked.setParent("Characters");

        //Uses the PlayerNetworked class and setActive function to determine if the model needs to be active or inactive
        //This is because some views in the Sample Scene need to hide the character models. 
        //This checks the local player's view and determines if it should be hidden
        currPlayerNetworked.SetActive();
                    
        //Debug to check that the character was actually spawned
        Debug.Log("Player Spawned");
    }


    //Moves the character and is called in the Update function if isMoving is true
    public void MoveCharacter(Vector3 position)
    {
        //Gets the grid location that the player selected to move to
        Vector3 movePointOnGrid = grid.getPositionOnGrid(position);

        //moves the character models position
        playerCharacter.transform.position = movePointOnGrid;
        


    }

    //Called from the Delete Character Button
    //Deletes the player's own character
    public void DeleteCharacter ()
    {
        PhotonNetwork.Destroy(playerCharacter);
    }

    //Called from the ChangeModel button
    public void ChangeModel()
    {
        //checks if the player already has a character and destroys the current model if they do
        if (playerCharacter != null)
        {
            PhotonNetwork.Destroy(playerCharacter);
        }

        //Starts spawning the new character model
        InitSpawn();
    }

    }



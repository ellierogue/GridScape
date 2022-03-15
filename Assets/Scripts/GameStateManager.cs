/*#-----------Wizards of the Codes Documentation-----------#
Contributors: Stephanie Simpson
Purpose: Manages what players can or can't see on their screen depending on their gamestate.
Game Objects Associated: SampleScene, GameStateManager gameobject
Files Associated: DiceNetowork.cs, PlayerNetworked.cs, ObjectNetworked.cs
Source: Self-Written
#----------------------#
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameStateManager : MonoBehaviour
{
    // Initial state to be shown (if the string is not a state in gameStates nothing will render).
    [SerializeField]
    private string initState;

    // List of possible states on the game
    [SerializeField]
    private List<GameState> gameStates;

    // Current state of the game
    private GameState currState;

    
    void Start()
    {
        // Loops through all game states, and sets all game objects within them to inactive
        foreach (GameState gamestate in gameStates)
        {
            // If the game state is the inital state, set it to current state
            if (gamestate.name == initState)
            {
                currState = gamestate;
            }

            foreach (GameStateObject gameStateObject in gamestate.gameStateObjects)
            {
                SetState(gameStateObject, false);
            }
        }

        // Make all game objects in current state active
        foreach (GameStateObject gameStateObject in currState.gameStateObjects)
        {
            //Checks which objects should be visible to the local player depending on their player status (DM, All Players, or just Players) and sets those to active.
            switch (gameStateObject.permissions)
            {
                //The DM is the one who created the lobby
                case Permissions.DM:
                    if (PhotonNetwork.IsMasterClient)
                    {
                        SetState(gameStateObject, true);
                    }
                    break;
                case Permissions.Players:
                    if (!PhotonNetwork.IsMasterClient)
                    {
                        SetState(gameStateObject, true);
                    }
                    break;
                
                 //All Players is the Default
                default:
                    SetState(gameStateObject, true);
                    break;
            }
        }
    }

    //Checks if an instantiated objects parent is in the active state for the local player
    public bool CheckActiveState(GameObject gameObject)
    {
        foreach (GameStateObject gameStateObject in currState.gameStateObjects)
        {
            //If the gameobject is a parent is in the active state than it will be set to active
            if (gameObject.transform.parent.gameObject == gameStateObject.gameObject && gameStateObject.isContainer == IsContainer.Container)
            {
                return true;
            }
        }
        //if it is not, it deactivates the instantiated child object
        return false;
    }

    public void SwitchState(string stateName)
    {
        // Sets all the objects in the current state to inactive
        foreach (GameStateObject gameStateObject in currState.gameStateObjects)
        {
            SetState(gameStateObject, false);
        }

        // Finds the new gamestate and sets all game objects inside it to active, and sets it to current state
        foreach (GameState gamestate in gameStates)
        {
            if (gamestate.name == stateName)
            {
                foreach (GameStateObject gameStateObject in gamestate.gameStateObjects) 
                {
                    switch (gameStateObject.permissions)
                    {
                        //Determines if the object should be visible depending on the player's status
                        case Permissions.DM:
                            if (PhotonNetwork.IsMasterClient)
                            {
                                SetState(gameStateObject, true);
                            }
                            break;
                        case Permissions.Players:
                            if (!PhotonNetwork.IsMasterClient)
                            {
                                SetState(gameStateObject, true);
                            }
                            break;
                        default:
                            SetState(gameStateObject, true);
                            break;
                    }
                }
                currState = gamestate;
            } 
        }
    }

    //Sets the active state for each gameobject, but will check if the gameobject is a parent
    //It will not turn off a parent object, but the status will update for all of the children which are the instantiated objects
    private void SetState(GameStateObject gameStateObject, bool isActive)
    {
        //The gameobject is a parent (container) and should not be turned off, but it's children gameobjects should be turned on and off
        if (gameStateObject.isContainer == IsContainer.Container)
        {
            foreach (Transform child in gameStateObject.gameObject.transform)
            {
                child.gameObject.SetActive(isActive);
            }
        }
        else
        {
            gameStateObject.gameObject.SetActive(isActive);
        }
    }
}


// GameState class contains the name of the state, and the list of game objects to be set to active when it's called
[Serializable]
public class GameState
{
    public string name;
    public List<GameStateObject> gameStateObjects;
}

//Settings for each gameobject in the gamestate
[Serializable]
public class GameStateObject
{
    //The gameobject from the hierarchy
    public GameObject gameObject;

    //Which players should be able to view the gameobject
    public Permissions permissions;

    //Whether the gameobject is a parent container for instantiated objects
    public IsContainer isContainer;
}

//Set which players can view an object
[Serializable]
public enum Permissions {
    AllPlayers,
    DM,
    Players
}

//Sets whether the object is a parent (container)
[Serializable]
public enum IsContainer
{
    Container,
    NotAContainer
}

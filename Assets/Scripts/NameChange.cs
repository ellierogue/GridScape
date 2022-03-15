/*#-----------Wizards of the Codes Documentation-----------#
Contributors: Stephanie Simpson
Purpose: Finds all active players in the room and instantiates a prefab with their name and status
Game Objects Associated: SampleScene, PlayerPrefab prefab object, PlayerManager gameobject, Content gameobject
Files Associated: SetPlayerInfo.cs
Source: Self-Written
#----------------------#
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;


public class NameChange : MonoBehaviour
{
    //Sets the prefab with the name and status text boxes
    public SetPlayerInfo playerPrefabs;
    
    //The container where the prefabs are to be instantiated
    public Transform playersContainer;

    //Creates a new prefab for each player with their name and status stored
    List<SetPlayerInfo> prefabList = new List<SetPlayerInfo>();

    int playerNum = 0;


    
    private void Start()
    {
        //Adds all players when first joining a room
        AddAllActivePlayers();
    }

  
    void Update()
    {
        //Checks whether players join or leave the room
        UpdateActivePlayers();
    }

    public void AddAllActivePlayers()
    {
        //Gets all players currently in the room
        foreach (KeyValuePair<int, Player> p in PhotonNetwork.CurrentRoom.Players)
        {
            //instantiates the PlayerPrefab
            SetPlayerInfo tempListing = Instantiate(playerPrefabs, Vector3.zero, Quaternion.identity, playersContainer);

            //Sets the player's name
            tempListing.SetPlayerName(p.Value);

            //adds the prefab to the list of prefabs
            prefabList.Add(tempListing);


        }
    }

    //updates names of players in the room
    public void UpdateActivePlayers()
    {
        //Destroys all prefabs currently in the list
        foreach (SetPlayerInfo item in prefabList)
        {
            Destroy(item.gameObject);
        }
        //Clears the list
        prefabList.Clear();


        //Instantiates all prefabs for each player name back into the list
        foreach (KeyValuePair<int, Player> p in PhotonNetwork.CurrentRoom.Players)
        {
            
            SetPlayerInfo tempListing = Instantiate(playerPrefabs, playersContainer.transform);
            tempListing.SetPlayerName(p.Value);

            //Checks if player created the lobby (DM)
            if (p.Value.IsMasterClient)
            {
                //Sets the DM text to active
                tempListing.SetDMStatus(p.Value);
                tempListing.DisableSelfStatus(p.Value);
            }

            //Checks if the player is the local player
            else if (p.Value.IsLocal)
            {
                //Sets the Self text to active
                tempListing.SetSelfStatus(p.Value);
                tempListing.DisableDMStatus(p.Value);
            }

            else
            {
                //For all other players, set both status textboxes to false
                tempListing.DisableSelfStatus(p.Value);
                tempListing.DisableDMStatus(p.Value);
            }


           //Add the prefab to the list
           //Increment number of players in the room
            prefabList.Add(tempListing);
            playerNum++;


        }


    }
}










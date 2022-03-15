/*#-----------Wizards of the Codes Documentation-----------#
Contributors: Trevor Pechous, Stephine Simpson
Purpose: Create and handle connections to the server, save user preferences for future use
Game Objects Associated:
Files Associated:
Source: Self-Written
#----------------------#
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public InputField createInput;
    public InputField joinInput;
    public InputField userName;



    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(createInput.text, new Photon.Realtime.RoomOptions() { BroadcastPropsChangeToAll = true, MaxPlayers = 6 }); //Create room with the specified user input as the name
    }

    public void JoinRoom() 
    {
        PhotonNetwork.JoinRoom(joinInput.text); // Join a room if one exists with the same specified user input name
    }

    public override void OnJoinedRoom()
    {
        
        //if (!PhotonNetwork.IsMasterClient)
        //{
            PhotonNetwork.LoadLevel("Character Profile"); //Once connected Load Character Creation Module
        //}

        /*else
        {
            PhotonNetwork.LoadLevel("SampleScene");
        }*/
    }

    void Update()
    {
        //Saves user input as variables
        PlayerPrefs.SetString("username", userName.text);

        //Sets player name to custom photon property so it can be called later in the player list in game
        PhotonNetwork.LocalPlayer.NickName = PlayerPrefs.GetString("username");

        PlayerPrefs.SetString("Cserver", createInput.text);
        PlayerPrefs.SetString("Jserver", joinInput.text);
    }
    void Awake()
    {
        //Loads saved variables if they exist to streamline the user experience and make reconnection to familiar rooms quick and easy
        if (PlayerPrefs.GetString("username") != "")
            userName.text = PlayerPrefs.GetString("username");
        if (PlayerPrefs.GetString("Cserver") != "")
            createInput.text = PlayerPrefs.GetString("Cserver");
        if (PlayerPrefs.GetString("Jserver") != "")
            joinInput.text = PlayerPrefs.GetString("Jserver");
    }


}

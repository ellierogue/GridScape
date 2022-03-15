/*#-----------Wizards of the Codes Documentation-----------#
Contributors: Stephanie Simpson
Purpose: Sets the player names text and their status for each prefab for each player
Game Objects Associated: SampleScene, PlayerPrefab prefab object, PlayerManager gameobject, Content gameobject
Files Associated: NameChange.cs
Source:
#----------------------#
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class SetPlayerInfo : MonoBehaviour
{
    int index;
            
    //Stores the players username
    public TMPro.TMP_Text playerName;
    
    //Set if the player is the DM
    public TMPro.TMP_Text DmStatus;
    
    //Set if the usename belongs to the player
    public TMPro.TMP_Text SelfStatus;

    //A list of colors that will represent the order that the players entered the room. The same color indexes are used to match the dice color
    public List <Color> textColor;

    //Sets the players name that was sent by the NameChange.cs script
    public void SetPlayerName(Player player)
    {
        //The nickname is the username that the player entered when they joined the lobby
        playerName.text = player.NickName;

        //The ActorNumber is the order players enter the lobby
        if (player.ActorNumber <= 6)
        {
            index = player.ActorNumber - 1;
        }

        else  if (player.ActorNumber <= 12)
        {
            index = player.ActorNumber - 7;
        }

        else
        {
            index = 5; 
        }
        
        //The color of the player's name is determined by the player's actor number
        Color colorName = textColor[index];

        playerName.color = colorName;
    }

    //Sets the DM text to true if they player created the lobby
    public void SetDMStatus(Player player)
    {
        DmStatus.gameObject.SetActive(true);
    }

    //Disables the DM status for all other players
    public void DisableDMStatus (Player player)
    {
        DmStatus.gameObject.SetActive(false);
    }

    //Sets the Me text to true if the player name belongs to the player unless they are the DM
    public void SetSelfStatus(Player player)
    {
        SelfStatus.gameObject.SetActive(true);
    }

    //Disables the Me text if the player name does not belong to the player
    public void DisableSelfStatus(Player player)
    {
        SelfStatus.gameObject.SetActive(false);
    }
}

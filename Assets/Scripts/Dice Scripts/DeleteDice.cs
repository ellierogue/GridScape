/*#-----------Wizards of the Codes Documentation-----------#
Contributors: Stephanie Simpson
Purpose: Clears the player's dice off dice tray
Game Objects Associated: SampleScene, DiceStorage, DiceController, ClearDiceBtn
Files Associated: 
Source: Self-Written
#----------------------#
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DeleteDice : MonoBehaviour
{
    
    //Is called when the player selects the ClearDiceBtn
   public void DeleteAllDice()
    {
        //Each dice prefab is instantiated with a "Dice" tag
        //Searches for all instantiated dice objects
        GameObject[] dice = GameObject.FindGameObjectsWithTag("Dice");

        //Destroys each dice that the local player instantiated
        foreach (GameObject die in dice)
        {
            PhotonNetwork.Destroy(die);
        }
    }
}

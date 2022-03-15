/*#-----------Wizards of the Codes Documentation-----------#
Contributors: Stephanie Simpson
Purpose: Clears board of instantiated objects
Game Objects Associated: SampleScene, Objects gameobject, Clear Board Button
Files Associated: 
Source: Self-Written
#----------------------#
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ClearBoard : MonoBehaviour
{
    //Clear objects from game board
    public void ClearAllBoard()
    {
        //Searches for all game objects tagged with "Object" and adds them to a list
        GameObject[] instantObjects =  GameObject.FindGameObjectsWithTag("Object");

        //Destroys each object in the list
        foreach (GameObject item in instantObjects)
        {
            PhotonNetwork.Destroy(item);
        }
    }


}

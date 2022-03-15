/*#-----------Wizards of the Codes Documentation-----------#
Contributors: Stephanie Simpson
Purpose: Gives players a unique die color to instantiate. Performs RPC calls to sync the colors across the network and have each player check if the die needs to be visible in their current view
Game Objects Associated: SampleScene, GameStateManager, Dice Prefabs (D4_fixed, D6_fixed, D8_fixed, D10_fixed, D12_fixed, D100_fixed, D20_fixed_), DiceStorage gameobject
Files Associated: GameStateManager, DiceController
Source: Self-Written
#----------------------#
*/
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceNetwork : MonoBehaviourPun
{
    //Stores a list of the possible prefab materials for the dice
    public List<Material> diceMaterials;

    //Gets the gameobject parent name (the one stored in the hierarchy) which will store the instantiated die
    public void SetParent(string parentName)
    {
        photonView.RPC("SetParentOnNetwork", RpcTarget.All, parentName);
    }

    //Has all players add the instantiated die as a child of the gameobject (should be the DiceStorage gameobject)
    [PunRPC]
    void SetParentOnNetwork(string parentName)
    {
        //Finds the parent gameobject in the hierarchy
        GameObject parent = GameObject.Find(parentName);
        
        //Adds the die to the parent object
        gameObject.transform.parent = parent.transform;
    }

    public void SetActive()
    {
        photonView.RPC("SetActiveOnNetwork", RpcTarget.All);
    }

    //Has each player determine if the instantiated die needs to be visible or hidden in their current GameState (view)
    [PunRPC]
    void SetActiveOnNetwork()
    {
        //Looks for the GameStateManager in the hierarchy
        GameStateManager gameStateManager = GameObject.Find("GameStateManager").GetComponent<GameStateManager>();

        //Uses the gameStateManager.cs function to check whether the die should be visible based on whether it's parent object should be active
        gameObject.SetActive(gameStateManager.CheckActiveState(gameObject));
    }

    //Sets the die material (color) of the player who instantiated it based on their player number in the room
    public void ChangeDiceMaterial()
    {
        photonView.RPC("ChangeDiceMaterialOnNetwork", RpcTarget.All, PhotonNetwork.LocalPlayer.ActorNumber);
    }

    //Has all players on the network change the die color based on the player who instanited it
    [PunRPC]
    void ChangeDiceMaterialOnNetwork(int playerNumber)
    {
        //Will be using an index value so subtract 1
        int index = playerNumber - 1;

        //Sets the initial material
        Material diceMat;
        diceMat = diceMaterials[0];

        //Sets the dicematerial based on the player number
        if (index < diceMaterials.Count)
        {
            diceMat = diceMaterials[index];
        }

        gameObject.GetComponent<MeshRenderer>().material = diceMat;
    }
}

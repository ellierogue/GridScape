/*#-----------Wizards of the Codes Documentation-----------#
Contributors: Stephanie Simpson with minor help from Daniel Ta
Purpose: Handles dice instantiation
Game Objects Associated: SampleScene, DiceController gameobject, DiceStorage gameobject, Dice Inventory Buttons (D4Button, D6Button, D8Button, D10Button, D100Button, D12Button, D20Button)   
Files Associated: DiceNetwork
Source: Self-Written
#----------------------#
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class DiceController : MonoBehaviour
{
    //holds all possible dice prefabs in the DiceController gameobject
    public GameObject[] dicePrefabs;

    //name of instantiated die added to DiceStorage gameobject
    private GameObject diceName;

    //the instantiated die
    GameObject diceToSpawn;


    //Called from each of the Dice Inventory Buttons and passes which dice prefab they correspond to
    public void InitSpawn(GameObject gDiceToSpawn)
    {
        Debug.Log("Dice began spawning");

        //Sets the prefab gameobject that was passed from the selected dice button
        diceToSpawn = gDiceToSpawn;
        
        SpawnDice();

    }


    //Spawns/Instantiates the selected die
    public void SpawnDice()
    {
        //instantiates the selected die based and sets the name, the location on the dice board and no rotation
        diceName = PhotonNetwork.Instantiate(diceToSpawn.name, new Vector3(38,4,166), Quaternion.identity,0);

        //Needs the DiceNetwork.cs script so that the dice can be set and handled for all player
        DiceNetwork diceNetworked = diceName.GetComponent<DiceNetwork>();
        
        //A DiceNetwork function that stores the instantiated die in the DiceStorage gameobject for all players
        diceNetworked.SetParent("DiceStorage");
        
        //A DiceNetwork function that determines if the die needs to be active or disabled for each player once it's instantiated based on the view of the local player
        diceNetworked.SetActive();
        
        //Changes the instantiated die material (the color of the die) based on the player number in the room so that each die corresponds to the instantiating player's color
        diceNetworked.ChangeDiceMaterial();
    }

}

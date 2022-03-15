/*#-----------Wizards of the Codes Documentation-----------#
Contributors: Stephanie Simpson
Purpose: Handles how the character is instantiated for all players, sets the character to a parent gameobject for all players and determines whether the character should be visible for each player
Game Objects Associated: SampleScene, Each character model prefab has this script attached
Files Associated: GameStateManager.cs, PlayerCharacterController.cs
Source: Self-Written
#----------------------#
*/using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerNetworked : MonoBehaviourPun
{
    //Called from the PlayerCharacterController.cs script when a character is instantiated and passes the parent gameobject name
    public void setParent(string parentName)
    {
        photonView.RPC("setParentOnNetwork", RpcTarget.All, parentName);
    }

    //Tells all players to store the character as a child of the Characters gameobject in the hierarchy
    [PunRPC]
    void setParentOnNetwork(string parentName)
    {
        GameObject parent = GameObject.Find(parentName);
        gameObject.transform.parent = parent.transform;
    }

    //Called from the PlayerCharacterController script when the character is instantiated
    public void SetActive()
    {
        photonView.RPC("SetActiveOnNetwork", RpcTarget.All);
    }

    //Tells every player to check whether the character model should be visible or not depending on which current gamestate they are viewing
    [PunRPC]
    void SetActiveOnNetwork()
    {
        GameStateManager gameStateManager = GameObject.Find("GameStateManager").GetComponent<GameStateManager>();
        gameObject.SetActive(gameStateManager.CheckActiveState(gameObject));
    }
}

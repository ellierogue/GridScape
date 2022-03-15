/*#-----------Wizards of the Codes Documentation-----------#
Contributors: Stephanie Simpson
Purpose: Handles how the object is instantiated for all players, sets the objects to a parent gameobject for all players and determines whether the object should be visible for each player
Game Objects Associated: SampleScene, Each object prefab has this script attached
Files Associated: ObjectsandNPC.cs, GameStateManager.cs
Source: Self-Written
#----------------------#
*/

using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectNetworked : MonoBehaviourPun
{
    //The range for the width of the outline
    [Range(0.0f, 10.0f)]
    public float outlineWidth;

    //Receives the parent gameobject name from the ObjectsandNPC script
    public void SetParent(string parentName)
    {
        //Calls function for all players
        photonView.RPC("setParentOnNetwork", RpcTarget.All, parentName);
    }

    //Sets the parent gameobject for all players which should be the Objects gameobject
    [PunRPC]
    void setParentOnNetwork(string parentName)
    {
        GameObject parent = GameObject.Find(parentName);
        gameObject.transform.parent = parent.transform;
    }

    //The ObjectsandNPC script calls this function when objects are instantiated
    public void SetActive()
    {
        photonView.RPC("SetActiveOnNetwork", RpcTarget.All);
    }

    //Checks whether the object should be visible for each player by checking whether it's parent gameobject should be visible
    [PunRPC]
    void SetActiveOnNetwork()
    {
        GameStateManager gameStateManager = GameObject.Find("GameStateManager").GetComponent<GameStateManager>();
        gameObject.SetActive(gameStateManager.CheckActiveState(gameObject));
    }

    //Whether the object is currently selected. Passed from the ObjectsandNPC class
    public void SetSelected(bool selected)
    {
        photonView.RPC("SetSelectedOnNetwork", RpcTarget.All, selected);
    }

    //Tells all players that the object that the DM is currently selected should have an outline or whether it has been deselected and the outline should be removed
    [PunRPC]
    void SetSelectedOnNetwork(bool selected)
    {
        Outline outline = gameObject.GetComponent<Outline>();

        //Add the outline
        if (selected)
        {
            outline.OutlineWidth = outlineWidth;
        } 
        
        //Remove the outline
        else
        {
            outline.OutlineWidth = 0.0f;
        }
    }
}

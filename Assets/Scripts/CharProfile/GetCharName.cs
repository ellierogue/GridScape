/*#-----------Wizards of the Codes Documentation-----------#
Contributors: Stephanie Simpson
Purpose: Sets the character's name on the Character Selection Scene
Game Objects Associated: SampleScene; Character Selection Scene, CharName text gameobject
Files Associated: 
Source: Self-Written
#----------------------#
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

//Requires Text Component to be on gameobject before script can be added
[RequireComponent(typeof(Text))]
public class GetCharName : MonoBehaviour
{
    Text text;

    void Start()
    {
        text = GetComponent<Text>();

        //Sets the Character's name as long as one was already entered (it should have been)
        if (PlayerPrefs.GetString("charname") != "")
            text.text = PlayerPrefs.GetString("charname");
    }


}

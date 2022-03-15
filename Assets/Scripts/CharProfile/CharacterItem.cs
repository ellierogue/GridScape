/*#-----------Wizards of the Codes Documentation-----------#
Contributors: Stephanie Simpson
Purpose: Sets values for our character profile, calls them back later when the profile is viewed again and updates the values if they are changed
Game Objects Associated: SampleScene, Character Profile Scene, HandleCharProfile gameobject, saveBtn gameobject
Files Associated:  
Source: Self-Written
#----------------------#
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

//This script is handled by the HandleCharProfile gameobject
//The values are updated when the saveBtn is selected
//Uses PlayerPrefs which are values that are saved on the local player's computer
public class CharacterItem : MonoBehaviourPunCallbacks

{

    public InputField charName;
    public TMP_InputField charRace;
    public Dropdown charClass;
    public TMP_InputField charHitPoints;
    public TMP_InputField charStrength;
    public TMP_InputField charDex;
    public TMP_InputField charConst;
    public TMP_InputField charIntell;
    public TMP_InputField charWisdom;
    public TMP_InputField charCharisma;
    public TMP_InputField charSummary;



    //The entered values are stored and updated when the player selects the Save Button
    public void SetCharacterValues()
    {

        PlayerPrefs.SetString("charname", charName.text);
        PlayerPrefs.SetString("charrace", charRace.text);
        PlayerPrefs.SetInt("charclass", charClass.value);
        PlayerPrefs.SetString("charhitpoints", charHitPoints.text);
        PlayerPrefs.SetString("charstrength", charStrength.text);
        PlayerPrefs.SetString("chardex", charDex.text);
        PlayerPrefs.SetString("charconst", charConst.text);
        PlayerPrefs.SetString("charintell", charIntell.text);
        PlayerPrefs.SetString("charwisdom", charWisdom.text);
        PlayerPrefs.SetString("charcharisma", charCharisma.text);
        PlayerPrefs.SetString("charsummary", charSummary.text);

        PhotonNetwork.LoadLevel("Character Selection");
    }

    public void ReturnLobby()
    {
        PhotonNetwork.Disconnect();
        PhotonNetwork.LoadLevel("Loading");
        
    }

    //used for the SampleScene to save the updated values when in the ViewProfile view
    //the game board button will call a click event to return to gameboard scene
    public void UpdateCharacterValues()
    {
        PlayerPrefs.SetString("charname", charName.text);
        PlayerPrefs.SetString("charrace", charRace.text);
        PlayerPrefs.SetInt("charclass", charClass.value);
        PlayerPrefs.SetString("charhitpoints", charHitPoints.text);
        PlayerPrefs.SetString("charstrength", charStrength.text);
        PlayerPrefs.SetString("chardex", charDex.text);
        PlayerPrefs.SetString("charconst", charConst.text);
        PlayerPrefs.SetString("charintell", charIntell.text);
        PlayerPrefs.SetString("charwisdom", charWisdom.text);
        PlayerPrefs.SetString("charcharisma", charCharisma.text);
        PlayerPrefs.SetString("charsummary", charSummary.text);
    }


    //When the scene is first called it sets the saved values if they were already set
    void Awake()
    {
        if (PlayerPrefs.GetString("charname") != "")
            charName.text = PlayerPrefs.GetString("charname");
        if (PlayerPrefs.GetString("charrace") != "")
            charRace.text = PlayerPrefs.GetString("charrace");
        if (PlayerPrefs.GetInt("charclass") != 0)
            charClass.value = PlayerPrefs.GetInt("charclass");
        if (PlayerPrefs.GetString("charhitpoints") != "")
            charHitPoints.text = PlayerPrefs.GetString("charhitpoints");
        if (PlayerPrefs.GetString("charstrength") != "")
            charStrength.text = PlayerPrefs.GetString("charstrength");
        if (PlayerPrefs.GetString("chardex") != "")
            charDex.text = PlayerPrefs.GetString("chardex");
        if (PlayerPrefs.GetString("charconst") != "")
            charConst.text = PlayerPrefs.GetString("charconst");
        if (PlayerPrefs.GetString("charintell") != "")
            charIntell.text = PlayerPrefs.GetString("charintell");
        if (PlayerPrefs.GetString("charwisdom") != "")
            charWisdom.text = PlayerPrefs.GetString("charwisdom");
        if (PlayerPrefs.GetString("charcharisma") != "")
            charCharisma.text = PlayerPrefs.GetString("charcharisma");
        if (PlayerPrefs.GetString("charsummary") != "")
            charSummary.text = PlayerPrefs.GetString("charsummary");

    }
}

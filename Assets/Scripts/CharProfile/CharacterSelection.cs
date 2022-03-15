/*#-----------Wizards of the Codes Documentation-----------#
Contributors: Stephanie Simpson
Purpose: Handles character selection to show a preview of the character model to the player before selection. Allows players to scroll through the options and select their model.
Game Objects Associated: SampleScene, Character Selection Scene, CharacterItem prefab gameobject, rightButton and leftButton gameobjects
Files Associated:
Source: Self-Written
#----------------------#
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using UnityEngine.UI;
using Photon.Pun;
public class CharacterSelection : MonoBehaviourPunCallbacks
{
    //Sets the left arrow gameobject that scrolls left through the list of available characters
    public GameObject leftArrowButton;

    //Sets the right arrow gameobject that scrolls right through the list of available characters
    public GameObject rightArrowButton;

    //Sets the initial placeholder image in the list of character models
    public Image playerCharacterAvatar;
    
    //Stores all of the possible avatars that will be in the list
    public Sprite[] avatars;


    //Checks if the player has previously selected a model and sets the image in the list if they have or shows the first image in the list if they haven't
    private void Start()
    {
        if (PlayerPrefs.HasKey("characteravatar"))
        {
            SetCharAvatar(PlayerPrefs.GetInt("characteravatar"));
        }
        else
        {
            PlayerPrefs.SetInt("characteravatar", 0);
        }
    }

    //Sets the visible avatar and stores the selected index of the avatar
    void SetCharAvatar(int value)
    {
        if (value == -1)
        {
            value = avatars.Length - 1;
        }
        //Shows the avatar image from avatars array based on the selected index 
        playerCharacterAvatar.sprite = avatars[value];
        
        //Stores the selected character's index as an int for the local player so it can be referenced later
        PlayerPrefs.SetInt("characteravatar", value);
    }

    //Scrolls through the array of avatars by decreasing int value when the left arrow is selected
    public void OnClickLeftArrow()
    {
        //Checks if already at the beginning of the list of sets the index to the end of the list of it is
        if (PlayerPrefs.GetInt("characteravatar") == 0)
        {
            SetCharAvatar(avatars.Length - 1);
        }
        else
        {
            SetCharAvatar(PlayerPrefs.GetInt("characteravatar") - 1);
        }
    }

    //Scrolls through the array of avatars by increasing int value when the right arrow is selected
    public void OnClickRightArrow()
    {
        //Checks if already at the end of the list and sets index back to 0 if it is
        if (PlayerPrefs.GetInt("characteravatar") == avatars.Length - 1)
        {
            SetCharAvatar(0);
        }
        else
        {
            SetCharAvatar(PlayerPrefs.GetInt("characteravatar") + 1);
        }
    }

    //When the Play Button is selected it calls the SampleScene Scene
    public void onPlayGameButton()
    {

       PhotonNetwork.LoadLevel("SampleScene");
        
    }

    //When the Back Button is selected it returns the player to the Character Profile Scene
    public void onBackButton()
    {
        PhotonNetwork.LoadLevel("Character Profile");
    }


}

        

 

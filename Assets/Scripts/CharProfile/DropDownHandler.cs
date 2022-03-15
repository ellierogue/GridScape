/*#-----------Wizards of the Codes Documentation-----------#
Contributors: Stephanie Simpson
Purpose: Handles the dropdownmenu in the character profile. Lets the ChangeProfileSprite.cs script know which image to set.
Game Objects Associated: CharacterProfileScene, SampleScene, ddlClass gameobject
Files Associated: ChangeProfileSprite.cs
Source: Self-Written
#----------------------#
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DropDownHandler : MonoBehaviour
{
    //Allows the dropdownlist that will be used to be set on the gameobject
    public Dropdown ddlClass;

    //Allows the ChangeProfileSprite.cs functions to be used
    //The class is what sets the initial sprite and changes the sprite based on user selection
    ChangeProfileSprite profileSprite;

    // This checks whether the dropdownlist had been previously changed from the starting value
    //if a value was selected previously it sets the selected corresponding profile image
    void Start()
    {
        profileSprite = gameObject.GetComponent<ChangeProfileSprite>();

        if (ddlClass.value > 0)
        {
            profileSprite.ChangeProfileImage(ddlClass.value);
        }

        ddlClass.onValueChanged.AddListener(delegate
        {
            ddlClassValueChangedHappened(ddlClass);
        });
    }

    //If the player selects a new value from the dropdown list the ChangeProfileImage from the ChangeProfileSprite.cs script is called and changes the profile image
    public void ddlClassValueChangedHappened(Dropdown sender)
    {

            profileSprite.ChangeProfileImage(ddlClass.value);
    }
}


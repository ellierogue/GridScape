/*#-----------Wizards of the Codes Documentation-----------#
Contributors: Stephanie Simpson
Purpose: Changes the profile images in character profile based on the value that the player selected from the dropdown list. Is called by the DropDownHandler.cs script to actually change the image
Game Objects Associated: CharacterProfileScene, SampleScene, imgProfileBackground object, ClassSpriteRenderer object, inside CharProfileCanvas prefab
Files Associated: DropdownHandler.cs
Source: Self-Written
#----------------------#
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//The script is called on the imgProfileBackground gameobject inside of the CharProfileCanvas prefab
public class ChangeProfileSprite : MonoBehaviour
{
    //Allows the original image to be set
    //In this case it's the brown inner circle inside the profile image frame
    public Image displayedImage;
    
    //Stores the image that will be changed when the player selects a new class in the dropdown
    public SpriteRenderer spriteRenderer;
    
    //Stores an array of all of the available profile images that correspond to the class dropdown list
    public Sprite[] profileArray;
    
    //Uses the DropDownHandler.cs script to change the profile image that corresponds to the selection the dropdownmenu
    public void ChangeProfileImage(int value)
    {
        spriteRenderer.sprite = profileArray[value];
        displayedImage.sprite = spriteRenderer.sprite;
    }

    // Gets the initial value that was set as the sprite renderer inside the imgProfileBackground gameobject
    void Start()
    {
       spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

}

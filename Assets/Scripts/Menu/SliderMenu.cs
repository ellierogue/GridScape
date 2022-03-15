/*#-----------Wizards of the Codes Documentation-----------#
Contributors: Julian Pineda, Stephanie Simpson
Purpose: When the Sliding Menu Button is pressed, move the entire Sliding Menu out of frame, or back into frame
Game Objects Associated: Sliding Menu
Files Associated: N/A
Source: https://www.youtube.com/watch?v=BKeva_IlUeE
#----------------------#
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderMenu : MonoBehaviour
{
    public GameObject PanelMenu;

    private bool sliderState;

    private void Awake()
    {
        Animator animator = PanelMenu.GetComponent<Animator>();
        animator.keepAnimatorControllerStateOnDisable = true;
    }

    public void ShowHideMenu()
    {
        
        if (PanelMenu != null)
        {
            Animator animator = PanelMenu.GetComponent<Animator>();
            if(animator != null)
            {
                sliderState = animator.GetBool("Show");
                animator.SetBool("Show", !sliderState);

            }
        }
    }

}

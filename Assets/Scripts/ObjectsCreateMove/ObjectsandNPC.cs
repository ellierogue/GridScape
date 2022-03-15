/*#-----------Wizards of the Codes Documentation-----------#
Contributors: Stephanie Simpson
Purpose: Handles the object instantiation, object movement, object selection, object deselection and object deletion
Game Objects Associated: SampleScene, ObjectController gameobject, Objects gameobject, Create Object Btn, Object Inventory Buttons, Delete Object Button, Clear Board Button
Files Associated: ObjectNetworked.cs
Source: Self-Written
#----------------------#
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
using UnityEngine.EventSystems;


public class ObjectsandNPC : MonoBehaviour
{ 


    //holds all possible object prefabs in the ObjectController gameobject
    public GameObject[] objectPrefabs;

    public EventSystem uiEventSystem; 

    //name of instantiated object added to Objects gameobject
    private GameObject objectName;

    //Whether the object is moving
    private bool isMoving = false;
    
    //Whether the object is being instantiated
    private bool isSpawning = false;
    
    //Used to get the grid spaces
    private Testing grid;

    //the current object is the one selected by a raycast which can be moved or deleted
    GameObject currObject;

    //Stores the previously selected object so it can be deselected and the outline can be removed
    GameObject prevObject;
    
    //This is set on each individual prefab to set which object prefab to instantiate when a new object button is selected/clicked in game
    GameObject objToSpawn;

    

    
   

    // Start is called before the first frame update
    void Start()
    {
        //find the testing grid so that the objects can be placed on it
        GameObject gridGameObj = GameObject.Find("Testing");
        grid = gridGameObj.GetComponent<Testing>();
    }


    // Update is called once per frame
    void Update()
    {
        if (isSpawning)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SpawnObject(Mouse3D.GetMouseWorldPosition());
                isSpawning = false;
            }
        }

        //Select current object using raycast so you can move or delete the object. Target object prefab must have an "Object" tag
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            //Checks if the mouse is selecting a UI object and returns if it is
            if (uiEventSystem.currentSelectedGameObject != null)
            {
                return;
            }

            //If the Move Object Button was selected and the currObject is being moved
            if (isMoving)
            {
                
                if (Input.GetMouseButtonDown(0))
                {
                    //Checks if the player has selected an object
                    if (currObject != null)

                    {
                        //Gets the grid space that the player selected
                        Vector3 position = Mouse3D.GetMouseWorldPosition();

                        Vector3 movePointOnGrid = grid.getPositionOnGrid(position);

                        //The object is moved to the new position
                        currObject.transform.position = movePointOnGrid;

                    }

                    //After the object has been moved it sets isMoving to false
                    isMoving = false;

                }
            }

            //Checks for the Object tag which is put on each "Object" prefab or the Buttons to Move and Delete Objects which are tagged with "ObjectBtn"
            else if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {
                    //If the user is selecting an object button, don't reselect another object or deselect that object
                    if (hit.collider.gameObject.CompareTag("ObjectBtn"))
                    {
                        return;
                    }

                    //Selects the object if it is tagged with "Object"
                    if (hit.collider.gameObject.CompareTag("Object"))
                    {
                        if (currObject != null)
                        {
                            //stores the previously selected object
                            prevObject = currObject;

                            //Uses an RPC function call to remove the outline from the previously selected object for all players
                            ObjectNetworked prevObjectNetworked = prevObject.GetComponent<ObjectNetworked>();
                            prevObjectNetworked.SetSelected(false);

                        }    
                        
                        //sets the new currently selected object
                        currObject = hit.collider.gameObject;

                        //Sets the outline for the selected object using an RPC function call for all players
                        ObjectNetworked currObjectNetworked = currObject.GetComponent<ObjectNetworked>();
                        currObjectNetworked.SetSelected(true);

                        Debug.Log("Current Object is " + currObject);
                        
                    }

                    //If the player clicked something that wasn't an object, the current object is deselected and the outline is removed
                    else
                    {
                        if (currObject != null)
                        {
                            ObjectNetworked currObjectNetworked = currObject.GetComponent<ObjectNetworked>();
                            currObjectNetworked.SetSelected(false);
                            currObject = null;
                        }
                    }

                }

                //If the raycast somehow didn't hit anything, the current object is deselected and the outline is removed
                else
                {
                    if (currObject != null)
                    {
                        ObjectNetworked currObjectNetworked = currObject.GetComponent<ObjectNetworked>();
                        currObjectNetworked.SetSelected(false);
                        currObject = null;
                    }
                }    
            }
        }
    }


    //Is called when the Move Object Button is selected
    public void InitMove()
    {

        isMoving = true;

    }

    //Is called when one of the Object Buttons in the Object Inventory is selected
    //Each button has the prefab object attached which is then passed to the InitSpawn function as gObjToSpawn
    public void InitSpawn(GameObject gObjToSpawn)
    {
        Debug.Log("Object began spawning");

        //Sets the prefab that needs to be instantiated depending on the button that was clicked
        objToSpawn = gObjToSpawn;
        
        //Sets isSpawning to true which is then checked in the Update function every frame
        isSpawning = true;
    }


    //This function is where the object is instantiated
    public void SpawnObject(Vector3 position)
    {
        //Gets the position on the grid that the player clicked on
        Vector3 spawnPointOnGrid = grid.getPositionOnGrid(position);

        //Instantiates the object
        GameObject objectToSpawn = objToSpawn;
        objectName = PhotonNetwork.Instantiate(objectToSpawn.name, spawnPointOnGrid, Quaternion.identity);

        //Uses the ObjectNetworked.cs class to set the parent of the instantiated object and determine whether the object should be active after instantiation
        ObjectNetworked currObjNetworked = objectName.GetComponent<ObjectNetworked>();
        currObjNetworked.SetParent("Objects");
        currObjNetworked.SetActive();


        Debug.Log("Player Spawned");
    }

    //Destroys the selected object
    public void DeleteObject()
    {
        PhotonNetwork.Destroy(currObject);
    }

    //Deselects the object
    public void DeSelectObject()
    {
        ObjectNetworked currObjectNetworked = currObject.GetComponent<ObjectNetworked>();
        
        //Will remove the outline and perform an RPC call to remove it for all players
        currObjectNetworked.SetSelected(false);
        
        currObject = null;

    }


}

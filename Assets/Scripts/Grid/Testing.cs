/*#-----------Wizards of the Codes Documentation-----------#
Contributors: Garon Ponder, Stephanie Simpson
Purpose: To instantiate grid and return points on grid
Game Objects Associated: Testing game object
Files Associated: Mouse3D.cs
Source: Self written by Garon and Stephanie
#----------------------#
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    public static Testing Instance { get; private set; }

    //Sizes grid for placement of objects on gameboard

    public int x = 16;
    public int z = 16;
    public float cellSize = 2.5f;


    GridXZ grid;

    private void Awake()
    {
        Instance = this;

        // Added Testing object's transform as parent so that grid will be drawn as part of Testing GameObject instead of at root
        grid = new GridXZ(x, z, cellSize, this.transform.position, this.transform);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Gets grid position on left mouse click
            grid.GetXZ(Mouse3D.GetMouseWorldPosition(), out int x, out int z);
        }
    }

    public Vector3 getPositionOnGrid(Vector3 position)
    {
        //gets grid position for other classes that need it

        grid.GetXZ(position, out int x, out int z);

        //ensures grid is contained to predetermined size, so that objects are not placed outside of it

        if (x < 0) { x = 0; }
        else if (x >= this.x) { x = this.x - 1; }

        if (z < 0) { z = 0; }
        else if (z >= this.z) { z = this.z - 1; }

        Vector3 gridSpace = grid.GetWorldPosition(x, 0, z);
        gridSpace.x += cellSize / 2;
        gridSpace.z += cellSize / 2;
        
        return gridSpace;
    }
}

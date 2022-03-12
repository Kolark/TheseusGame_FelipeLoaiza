using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    GridObject level;


    [SerializeField] Transform squaresParent;
    [SerializeField] GameObject square;

    [SerializeField] Transform wallsParent;
    [SerializeField] GameObject horizontalWall;
    [SerializeField] GameObject verticalWall;

    private List<List<Transform>> squareTransforms;
    private List<List<Transform>> verticalWalls;
    private List<List<Transform>> horizontalWalls;

    private bool instantiateObjects = false;

    //Public Methods
    
    public void SetCurrentGrid(GridObject level)
    {
        this.level = level;
        if (!instantiateObjects)
        {//First time
            GenerateGrid();
            GenerateWalls();
            instantiateObjects = true;
        }
        ActivateWalls();
    }
    public bool TryMove(Vector2Int currentPos,Vector2Int desiredPos)
    {
        bool isInside = desiredPos.x >= 0
            && desiredPos.y >= 0
            && desiredPos.x < level.Columns
            && desiredPos.y < level.Rows;

        Vector2Int diff = desiredPos - currentPos;

        if (!isInside) return false;

        bool canMove = false;
        if (diff.x < 0)//Moves left - Checks current
        {
            canMove = !level.Grid[currentPos.y].cols[currentPos.x].leftWall;
        }
        else if (diff.x > 0)//Moves right - Checks desired
        {
            canMove = !level.Grid[desiredPos.y].cols[desiredPos.x].leftWall;
        }

        if (diff.y < 0)//Moves down - Checks desired
        {
            canMove = !level.Grid[desiredPos.y].cols[desiredPos.x].topWall; 
        }
        else if (diff.y > 0)//Moves up - checks current
        {
            canMove = !level.Grid[currentPos.y].cols[currentPos.x].topWall;
        }

        return canMove;
 
    }
    //Returns coordinates of grid square.
    public Vector3 GetPosition(Vector2Int pos)
    {
        return squareTransforms[pos.y][pos.x].position;
    }
    //-------------

    /// <summary>
    /// Generates the grid, centered
    /// </summary>
    private void GenerateGrid()
    {
        float offSetRows = (level.Rows) / 2f;
        float offSetColumns = (level.Columns) / 2f;

        squareTransforms = new List<List<Transform>>(level.Rows);
        for (int i = 0; i < level.Rows; i++)
        {
            squareTransforms.Add(new List<Transform>(level.Columns));
        }

        for (int i = 0; i < level.Rows; i++)
        {
            for (int u = 0; u < level.Columns; u++)
            {
                GameObject squareObj =
                    Instantiate(square,
                    Vector3.up * (i - offSetRows) + Vector3.right * (u - offSetColumns) + new Vector3(0.5f, 0.5f),
                    Quaternion.identity);
                squareObj.name = $"Square:{i},{u}";
                squareTransforms[i].Add(squareObj.transform);
                squareObj.transform.parent = squaresParent;
            }
        }
    }
    //Generates the walls
    private void GenerateWalls()
    {
        float offSetRows = (level.Rows) / 2f;
        float offSetColumns = (level.Columns) / 2f;

        verticalWalls = new List<List<Transform>>(level.Rows);
        horizontalWalls = new List<List<Transform>>(level.Rows);

        for (int i = 0; i < level.Rows; i++)
        {
            verticalWalls.Add(new List<Transform>(level.Columns));
            horizontalWalls.Add(new List<Transform>(level.Columns));
        }

        for (int i = 0; i < level.Rows; i++)
        {
            for (int u = 0; u < level.Columns; u++)
            {
                Vector3 squarePosition = Vector3.up * (i - offSetRows) + Vector3.right * (u - offSetColumns) + new Vector3(0.5f, 0.5f);
                GridSquare gridSquare = level.Grid[i].cols[u];
                GameObject leftWall = Instantiate(verticalWall, squarePosition + (Vector3.left / 2), Quaternion.identity);
                leftWall.name = $"VerticalWall:{i},{u}";
                leftWall.transform.parent = wallsParent;

                GameObject topWall = Instantiate(horizontalWall, squarePosition + (Vector3.up / 2f), Quaternion.identity);
                topWall.name = $"HorizontalWall:{i},{u}";
                topWall.transform.parent = wallsParent;

                verticalWalls[i].Add(leftWall.transform);
                horizontalWalls[i].Add(topWall.transform);
            }
        }
    }
    //Activates CurrentWalls
    public void ActivateWalls()
    {
        for (int i = 0; i < level.Rows; i++)
        {
            for (int u = 0; u < level.Columns; u++)
            {
                GridSquare gridSquare = level.Grid[i].cols[u];
                verticalWalls[i][u].gameObject.SetActive(gridSquare.leftWall);
                horizontalWalls[i][u].gameObject.SetActive(gridSquare.topWall);
            }
        }
    }
}

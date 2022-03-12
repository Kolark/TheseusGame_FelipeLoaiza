using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu( fileName = "gridObj",menuName = "grid/create Grid",order =0)]
public class GridObject : ScriptableObject
{
    //Player and Enemy Pos
    [SerializeField] Vector2Int playerPos;
    [SerializeField] Vector2Int enemyPos;
    [SerializeField] Vector2Int exitPos;
    [SerializeField] string message;
    [SerializeField,HideInInspector] List<Row> grid = new List<Row>();

    [SerializeField] bool pressThisBeforeSave;
    public int Rows => GlobalSettings.Rows;
    public int Columns => GlobalSettings.Columns;

    public List<Row> Grid => grid;
    public Vector2Int PlayerPos => playerPos;
    public Vector2Int EnemyPos => enemyPos;
    public Vector2Int ExitPos => exitPos;
    public string Message => message;

#if UNITY_EDITOR
    public void UpdateGrid()
    {
   
        int diffRows = GlobalSettings.Rows - grid.Count;

        //If the amount decreases
        if (grid.Count > 0)
        {
            int diffCols = GlobalSettings.Columns - grid[0].cols.Count;
            if (diffCols < 0)
            {
                RemoveCols(Mathf.Abs(diffCols));
            }//If the amount increases
            else if (diffCols > 0)
            {
                AddCols(diffCols);
            }
        }


        //If the amount decreases
        if (diffRows < 0)
        {
            RemoveRows(Mathf.Abs(diffRows));
        }//If the amount increases
        else if (diffRows > 0)
        {
            AddRows(diffRows);
        }

    }

    private void AddRows(int amount)
    {
        for (int u = 0; u < amount; u++)
        {
            List<GridSquare> cols = new List<GridSquare>(GlobalSettings.Columns);
            for (int i = 0; i < GlobalSettings.Columns; i++)
            {
                cols.Add(new GridSquare());
            }
            grid.Add(new Row { cols = cols });
        }
    }
    private void RemoveRows(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            grid.RemoveAt(grid.Count - 1);
        }
    }

    private void AddCols(int amount)
    {
        for (int i = 0; i < grid.Count; i++)
        {
            for (int u = 0; u < amount; u++)
            {
                grid[i].cols.Add(new GridSquare());
            }
        }
    }

    private void RemoveCols(int amount)
    {
        for (int i = 0; i < grid.Count; i++)
        {
            for (int u = 0; u < amount; u++)
            {
                grid[i].cols.RemoveAt(grid[i].cols.Count - 1);
            }
        }
    }
#endif
}

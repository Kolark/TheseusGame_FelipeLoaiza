using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu( fileName = "gridObj",menuName = "grid/create Grid",order =0)]
public class GridObject : ScriptableObject
{
    //Player,Enemy and Exit Positions //aaa
    [SerializeField] Vector2Int playerPos;
    [SerializeField] Vector2Int enemyPos;
    [SerializeField] Vector2Int exitPos;
    //Victory Message
    [SerializeField] string message;
    //Grid Data
    [SerializeField,HideInInspector] List<Row> grid = new List<Row>();

    //Since the custom editor is not binded to the grid property, this allows us to manually save it.
    [SerializeField] bool pressThisBeforeSave;

    //Getters
    public int Rows => GlobalSettings.Rows;
    public int Columns => GlobalSettings.Columns;

    public List<Row> Grid => grid;
    public Vector2Int PlayerPos => playerPos;
    public Vector2Int EnemyPos => enemyPos;
    public Vector2Int ExitPos => exitPos;
    public string Message => message;

#if UNITY_EDITOR
    //In case the amount of rows and columns changes this updates it 
    //without destroying the current ones
    public void UpdateGrid()
    {
        int diffRows = GlobalSettings.Rows - grid.Count;
        foreach (Row row in grid)
        {
            int diffCols = GlobalSettings.Columns - grid[0].cols.Count;
            if (diffCols < 0){ Remove(row.cols, -diffCols); }//If the amount increases
            else if (diffCols > 0){ Add<GridSquare>(row.cols,()=>new GridSquare(), diffCols); }
        }
        //If the amount decreases
        if (diffRows < 0) { Remove(grid, -diffRows); }//If the amount increases
        else if (diffRows > 0)
        {
            Add<Row>(grid,()=> 
            {
                List<GridSquare> cols = new List<GridSquare>(GlobalSettings.Columns);
                for (int i = 0; i < GlobalSettings.Columns; i++) { cols.Add(new GridSquare());}
                return new Row { cols = cols };
            }, diffRows);
        }
    }
    //void Add<T>(IList myList, Func<T> constructor) { myList.Add(constructor()); }
    private void Add<T>(IList myList, Func<T> constructor,int amount) { for (int u = 0; u < amount; u++){ myList.Add(constructor());} }

    private void Remove(IList list,int amount) { for (int u = 0; u < amount; u++){ list.RemoveAt(list.Count - 1);} }

#endif
}

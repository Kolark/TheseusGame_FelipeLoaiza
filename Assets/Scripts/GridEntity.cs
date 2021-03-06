using UnityEngine;
using System.Collections.Generic;
public class GridEntity : MonoBehaviour
{
    //Saved old positions, allows it to undo      bbbbbbbbbbb
    protected Stack<Vector2Int> recordedPositions;

    protected Vector2Int currentPos;

    public Vector2Int GetCurrentPos => currentPos;

    //protected bool moveSuccessful = false;

    /// WasItSuccesful, Direction
    public System.Action<bool,Vector2Int> onMove;
    protected GridController grid;
    //Initialized Grid Entity, passing it's parameters needed.
    public virtual void INIT(GridController grid)
    {
        this.grid = grid;
    }

    //Sets Grid Entity Position, Called from outside the object
    public void SetPosition(Vector2Int initialPos)
    {
        if (grid == null)
        {
            throw new System.Exception("[Grid] is null, not Initialized");
        }
        this.recordedPositions = new Stack<Vector2Int>();
        this.currentPos = initialPos;
        transform.position = grid.GetPosition(currentPos);
    }
    //Goes back to a previous position
    public virtual void UndoMovement()
    {
        if (recordedPositions.Count == 0) return; 
        currentPos = recordedPositions.Pop();
        transform.position = grid.GetPosition(currentPos);
    }

    public bool Move(Vector2Int dir)
    {
        recordedPositions.Push(currentPos);
        bool tryMove = grid.TryMove(currentPos, currentPos + dir);
        if (tryMove)
        {
            NewMove(dir);

        }
        onMove?.Invoke(tryMove, dir);

        return tryMove;
    }

   
    //Generalized move method
    private void NewMove(Vector2Int dir)
    {
        currentPos += dir;
        transform.position = grid.GetPosition(currentPos);
    }
    
}


////Tries to MoveLeft;
//private void MoveLeft()
//{
//    recordedPositions.Push(currentPos);
//    bool tryMove = grid.TryMove(currentPos, currentPos + Vector2Int.left);
//    if (tryMove)
//    {
//        NewMove(Vector2Int.left);
//    }
//    onMove?.Invoke(tryMove, Vector2Int.left);
//}
////Tries to MoveRight;
//private void MoveRight()
//{
//    recordedPositions.Push(currentPos);
//    bool tryMove = grid.TryMove(currentPos, currentPos + Vector2Int.right);
//    if (tryMove)
//    {
//        NewMove(Vector2Int.right);
//    }
//    onMove?.Invoke(tryMove, Vector2Int.right);
//}
////Tries to MoveUp
//private void MoveUp()
//{
//    recordedPositions.Push(currentPos);
//    bool tryMove = grid.TryMove(currentPos, currentPos + Vector2Int.up);
//    if (tryMove)
//    {
//        NewMove(Vector2Int.up);
//    }
//    onMove?.Invoke(tryMove, Vector2Int.up);
//}
////Tries to MoveDown;
//private void MoveDown()
//{
//    recordedPositions.Push(currentPos);
//    bool tryMove = grid.TryMove(currentPos, currentPos + Vector2Int.down);
//    if (tryMove)
//    {
//        NewMove(Vector2Int.down);
//    }
//    onMove?.Invoke(tryMove, Vector2Int.down);
//}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : GridEntity
{
    //Amount of time to wait per move
    WaitForSeconds wait = new WaitForSeconds(0.35f);
    //The amount of steps it takes per movement
    [SerializeField] int stepsPerMovement;
    GridEntity target;
    public System.Action onTurnFinished;

    //Set Target to follow.
    public void SetTarget(GridEntity target)
    {
        this.target = target;
        this.onMove += OnAfterMove;
    }

    //Move a defined number of times
    public void Move()
    {
        StartCoroutine(MoveCoroutine());
    }
    //Note: Ideally i would use a Tweening library to move elements, since i didn't bothered with animations i figured this 
    //is the best next solution
    private IEnumerator MoveCoroutine()
    {
        for (int i = 0; i < stepsPerMovement; i++)
        {
            Step();
            yield return wait;
        }
        onTurnFinished?.Invoke();
    }
    //Revers movement in this case the amount determined by stepsPerMovement
    public override void UndoMovement()
    {
        for (int i = 0; i < stepsPerMovement; i++)
        {
            base.UndoMovement();
        }
    }
    //One Single Movement
    private void Step()
    {
        //If not vertifcally aligned and can't move horizontally,
        int diffX = target.GetCurrentPos.x - currentPos.x;
        int diffY = target.GetCurrentPos.y - currentPos.y;
        if (diffX == 0 && diffY == 0) return;
        if(diffX != 0)//X axis not aligned
        {

            if(diffX > 0)//target to the right
            {
                //MoveRight();
                Move(Vector2Int.right);
            }
            else //target to the left
            {
                //MoveLeft();
                Move(Vector2Int.left);
            }
            
        }
        else//X axis aligned
        {
            TryMoveVertical(diffY);
        }
    }

    private void OnAfterMove(bool succes, Vector2Int dir)
    {
        int diffY = target.GetCurrentPos.y - currentPos.y;
        if(!succes && dir.x != 0 && diffY != 0)
        {
            TryMoveVertical(diffY);
        }
    }
    //Tries to move Vertical
    private void TryMoveVertical(int diffY)
    {
        if (diffY > 0)//target went up
        {
            //MoveUp();
            Move(Vector2Int.up);
        }
        else //target went down
        {
            //MoveDown();
            Move(Vector2Int.down);
        }
    }
}

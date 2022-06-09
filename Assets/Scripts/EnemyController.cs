using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : GridEntity
{
    //Amount of time to wait per move //aaaaaaaaaaaa
    WaitForSeconds wait = new WaitForSeconds(0.35f);
    //The amount of steps it takes per movement
    [SerializeField] int stepsPerMovement;
    GridEntity target;
    public System.Action onTurnFinished;

    private bool successfulMove = false;

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
            if (Step())
            {
                yield return wait;
            }
            else yield return null;
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
    private bool Step()
    {
        //If not vertifcally aligned and can't move horizontally,
        int diffX = target.GetCurrentPos.x - currentPos.x;
        int diffY = target.GetCurrentPos.y - currentPos.y;
        if (diffX == 0 && diffY == 0) return false;

        if(diffX != 0)//X axis not aligned
        {

            if(diffX > 0)//target to the right
            {
                //MoveRight();
                return Move(Vector2Int.right);
            }
            else //target to the left
            {
                //MoveLeft();
                return Move(Vector2Int.left);
            }
            
        }
        else//X axis aligned
        {
            return TryMoveVertical(diffY);
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
    private bool TryMoveVertical(int diffY)
    {
        if (diffY > 0)//target went up
        {
            //MoveUp();
            return Move(Vector2Int.up);
        }
        else //target went down
        {
            //MoveDown();
            return Move(Vector2Int.down);
        }
    }
}

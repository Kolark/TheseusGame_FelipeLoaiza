using UnityEngine;

public class PlayerController : GridEntity
{
    Inputs inputs;
    public Inputs Inputs => inputs;
    public override void INIT(GridController grid)
    {
        base.INIT(grid);
        inputs = GetComponent<Inputs>();
    }
    //Sets Events that are triggered by the Inputs Class
    public void SetEvents(System.Action onWait)
    {
        inputs.onLeftArrow += MoveLeft;
        inputs.onRightArrow += MoveRight;
        inputs.onDownArrow += MoveDown;
        inputs.onUpArrow += MoveUp;
        inputs.onWait += ()=> 
        {
            recordedPositions.Push(currentPos);
            inputs.Disable();
            onWait?.Invoke();
        } ;
    }
    private void OnDestroy()
    {
        inputs.onLeftArrow = null;
        inputs.onRightArrow = null;
        inputs.onDownArrow = null;
        inputs.onUpArrow = null;
        inputs.onWait = null;
    }
}

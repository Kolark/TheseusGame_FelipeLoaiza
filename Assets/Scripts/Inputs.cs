using UnityEngine;
using System;
/// <summary>
/// Simple Inputs class
/// </summary>
public class Inputs : MonoBehaviour
{
    //
    //rpc
    //Use 1 paramater

    //events fired after input
    //public Action onUpArrow;
    //public Action onDownArrow;
    //public Action onLeftArrow;
    //public Action onRightArrow;
    public Action onWait;

    public Action<Vector2Int> onMovementInput;

    public bool isActive = true;
    // Update is called once per frame
    void Update()
    {
        if (!isActive) return;
        //Listens to key events and triggers Events
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            OnMovement(Vector2Int.up);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            OnMovement(Vector2Int.down);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            OnMovement(Vector2Int.left);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            OnMovement(Vector2Int.right);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            OnWait();
        }
    }


    public void OnMovement(Vector2Int dir)
    {
        if (!isActive) return;
        onMovementInput?.Invoke(dir);
    }
    //                      0               1                   2               3
    Vector2Int[] dirs = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };

    public void OnMovement(int index)
    {
        if (!isActive) return;
        onMovementInput?.Invoke(dirs[index]);
    }


    public void OnWait()
    {
        if (!isActive) return;
        onWait?.Invoke();
    }
    //Allows Update to listen to key Events
    public void Enable()
    {
        isActive = true;
    }

    //Stops update from listening to key Events
    public void Disable()
    {
        isActive = false;
    }
}



////Methods accesible both by script and buttons
//public void OnUp()
//{
//    if (!isActive) return;
//    onMovementInput?.Invoke(Vector2.up);
//    //onUpArrow?.Invoke();
//}
//public void OnDown()
//{
//    if (!isActive) return;
//    onMovementInput?.Invoke(Vector2.down);
//    //onDownArrow?.Invoke();
//}
//public void OnRight()
//{
//    if (!isActive) return;
//    onMovementInput?.Invoke(Vector2.right);
//    //onRightArrow?.Invoke();
//}
//public void OnLeft()
//{
//    if (!isActive) return;
//    onMovementInput?.Invoke(Vector2.left);
//    //onLeftArrow?.Invoke();
//}


//public void myfunc(object myobj)
//{
//    bool a = 5 == 5;
//    Vector2 b = Vector2.up + Vector2.down;
//    if (typeof(object) == typeof(Vector2))//by reference
//    {
//        Vector2 vec2 = (Vector2)myobj;
//    }

//    //myobj.Equals()//by value
//}
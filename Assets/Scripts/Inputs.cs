using UnityEngine;
using System;
/// <summary>
/// Simple Inputs class
/// </summary>
public class Inputs : MonoBehaviour
{
    //events fired after input
    public Action onUpArrow;
    public Action onDownArrow;
    public Action onLeftArrow;
    public Action onRightArrow;
    public Action onWait;
    public bool isActive = true;
    // Update is called once per frame
    void Update()
    {
        if (!isActive) return;
        //Listens to key events and triggers Events
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            OnUp();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            OnDown();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            OnLeft();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            OnRight();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            OnWait();
        }
    }

    //Methods accesible both by script and buttons
    public void OnUp()
    {
        if (!isActive) return;
        onUpArrow?.Invoke();
    }
    public void OnDown()
    {
        if (!isActive) return;
        onDownArrow?.Invoke();
    }
    public void OnRight()
    {
        if (!isActive) return;
        onRightArrow?.Invoke();
    }
    public void OnLeft()
    {
        if (!isActive) return;
        onLeftArrow?.Invoke();
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

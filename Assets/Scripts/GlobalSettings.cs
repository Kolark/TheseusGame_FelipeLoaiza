using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Global Settings for the game, had to make it static since i don't know how to add a serializedField in a customEditor
public static class GlobalSettings
{
    //Grid Size
    public static int columnsAmount = 10;
    public static int rowsAmount = 10;

    //Getters
    public static int Columns => columnsAmount;
    public static int Rows => rowsAmount;
}

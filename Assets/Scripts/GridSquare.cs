using System.Collections.Generic;
//Data that determines whether a Square in the Grid should have walls
//NOTE to self: Could technically have as well rightWall and bottomWall, 
//which would make things easier, however it would make it harder to visualize. 
[System.Serializable]
public struct GridSquare 
{
    public bool leftWall;
    public bool topWall;
}
[System.Serializable]
public struct Row
{
    public List<GridSquare> cols;
}
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;


public class SquareEditor : VisualElement
{
    const string ON_CLASS = "On";
    const string OFF_CLASS = "Off";
    //Buttons in the Square
    private Button leftWall;
    private Button topWall;
    //Current State
    GridSquare currentState;
    //Determined pos
    Vector2Int pos;
    public System.Action<Vector2Int,GridSquare> onStateChanged;
    public SquareEditor(Vector2Int pos,GridSquare gridSquare)
    {
        currentState = gridSquare;
        this.pos = pos;
        VisualTreeAsset visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/SquareEditor.uxml");
        visualTree.CloneTree(this);

        leftWall = this.Q<Button>("LeftWall");
        topWall = this.Q<Button>("TopWall");

        leftWall.clicked += () =>
        {
            currentState.leftWall = !currentState.leftWall;
            onStateChanged?.Invoke(pos,currentState);
            RenderCurrentState();
        };

        topWall.clicked += () =>
        {
            currentState.topWall = !currentState.topWall;
            onStateChanged?.Invoke(pos, currentState);
            RenderCurrentState();
        };
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/SquareEditor.uss");
        this.styleSheets.Add(styleSheet);

        RenderCurrentState();
    }


    private void RenderCurrentState()
    {
        if (currentState.leftWall)//Its On
        {
            leftWall.RemoveFromClassList(OFF_CLASS);
            leftWall.AddToClassList(ON_CLASS);
        }
        else //Its Off
        {
            leftWall.RemoveFromClassList(ON_CLASS);
            leftWall.AddToClassList(OFF_CLASS);
        }

        if (currentState.topWall)//Its On
        {
            topWall.RemoveFromClassList(OFF_CLASS);
            topWall.AddToClassList(ON_CLASS);
        }
        else //Its Off
        {
            topWall.RemoveFromClassList(ON_CLASS);
            topWall.AddToClassList(OFF_CLASS);
        }
    }
}
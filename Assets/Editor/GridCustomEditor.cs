using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.Collections.Generic;
[CustomEditor(typeof(GridObject))]
public class GridCustomEditor : Editor
{
    private GridObject grid;
    private VisualElement _root;
    private VisualTreeAsset _visualTree;
    private VisualElement container;
    private List<List<SquareEditor>> squareEditors;
    private Vector2Int playerPos;
    private Vector2Int enemyPos;
    private Vector2Int exitPos;
    private void OnEnable()
    {
        grid = target as GridObject;

        //Alocates Memory
        squareEditors = new List<List<SquareEditor>>(grid.Rows);

        _root = new VisualElement();
        
        _visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/GridCustomEditor.uxml");
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/GridCustomEditor.uss");

        _root.styleSheets.Add(styleSheet);
    }

    public override VisualElement CreateInspectorGUI()
    {
        var root = _root;
        _visualTree.CloneTree(root);
        container = root.Q<VisualElement>("Container");

        var PlayerPos = root.Q<Vector2IntField>("PlayerPos");
        var EnemyPos = root.Q<Vector2IntField>("EnemyPos");
        var ExitPos = root.Q<Vector2IntField>("ExitPos");
        var Message = root.Q<TextField>("Message");

        PlayerPos.BindProperty(serializedObject.FindProperty("playerPos"));
        EnemyPos.BindProperty(serializedObject.FindProperty("enemyPos"));
        ExitPos.BindProperty(serializedObject.FindProperty("exitPos"));
        Message.BindProperty(serializedObject.FindProperty("message"));

        PlayerPos.RegisterValueChangedCallback((e) =>
        {
            playerPos = e.newValue;
            RenderContainer();
        });

        EnemyPos.RegisterValueChangedCallback((e) =>
        {
            enemyPos = e.newValue;
            RenderContainer();
        });

        ExitPos.RegisterValueChangedCallback((e) =>
        {
            exitPos = e.newValue;
            RenderContainer();
        });

        grid.UpdateGrid();
        RenderContainer();
        return root;
    }

    private void RenderContainer()
    {
        container.Clear();
        squareEditors = new List<List<SquareEditor>>();
        for (int i = 0; i < grid.Rows; i++)
        {
            squareEditors.Add(new List<SquareEditor>(grid.Columns));
        }

        for (int i = 0; i < grid.Rows; i++)//Rows
        {
            VisualElement row = new VisualElement();
            for (int u = 0; u < grid.Columns; u++)//Columns
            {
                SquareEditor square = new SquareEditor(new Vector2Int(i,u),grid.Grid[i].cols[u]);
                if(playerPos.x == u && playerPos.y == i)
                {
                    square.style.backgroundColor = new StyleColor(Color.green);
                }
                if (enemyPos.x == u && enemyPos.y == i)
                {
                    square.style.backgroundColor = new StyleColor(Color.red);
                }
                if (exitPos.x == u && exitPos.y == i)
                {
                    square.style.backgroundColor = new StyleColor(Color.yellow);
                }
                square.onStateChanged += OnSquareStateChange;
                squareEditors[i].Add(square);
                row.Add(square);
            }
            row.style.flexWrap = new StyleEnum<Wrap>(Wrap.NoWrap);
            row.style.flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Row);
            row.style.borderRightWidth = new StyleFloat(1f);
            row.style.borderLeftWidth = new StyleFloat(1f);
            row.style.borderTopWidth = new StyleFloat(1f);
            row.style.borderBottomWidth = new StyleFloat(1f);
            row.style.borderRightColor = new StyleColor(Color.red);
            row.style.borderLeftColor = new StyleColor(Color.red);
            container.Insert(0,row);
        }
    }

    private void OnSquareStateChange(Vector2Int pos,GridSquare state)
    {
        grid.Grid[pos.x].cols[pos.y] = state;
    }
}
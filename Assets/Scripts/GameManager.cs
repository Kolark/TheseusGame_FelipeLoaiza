using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Entities
    [SerializeField] PlayerController player;
    [SerializeField] EnemyController enemy;
    [SerializeField] Transform exit;

    [SerializeField] GridController grid;
    [SerializeField] GridObject[] levels;

    [SerializeField] UiController uiController;
    GameStates currentState = GameStates.Playing;

    private int currentLevel = 0;

    private void Awake()
    {
        grid.SetCurrentGrid(levels[currentLevel]);

        player.INIT(grid);
        player.SetEvents(enemy.Move);
        player.onMove += OnPlayerMove;

        enemy.INIT(grid);
        enemy.SetTarget(player);
        enemy.onTurnFinished += OnEnemyEndedTurn;
        SetupLevel();
    }

    private void Update()
    {
        //Debug
        if (Input.GetKeyDown(KeyCode.R))
        {
            SetupLevel();
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            UndoLastMovement();
        }
        if (Input.GetKeyDown(KeyCode.Plus) || Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            NextLevel();
        }
        if (Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            PreviousLevel();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Continue();
        }
    }
    //Setups walls, and initial positions for both player and Enemy
    public void SetupLevel()
    {
        grid.ActivateWalls();
        player.Inputs.Enable();
        uiController.HideAll();
        player.SetPosition(levels[currentLevel].PlayerPos);
        enemy.SetPosition(levels[currentLevel].EnemyPos);
        exit.transform.position = grid.GetPosition(levels[currentLevel].ExitPos);
    }
    //Revers the last move on gridEntities
    public void UndoLastMovement()
    {
        player.UndoMovement();
        enemy.UndoMovement();
    }
    //Increased the currentLevel Counter and activates the walls of the now CurrentLevel
    public void NextLevel()
    {
        currentLevel = Mathf.Clamp(currentLevel + 1, 0, levels.Length - 1);
        uiController.HideAll();

        SetupLevel();
    }
    //Decreases the currentLevel Counter and activates the walls of the now CurrentLevel
    public void PreviousLevel()
    {
        currentLevel = Mathf.Clamp(currentLevel - 1, 0, levels.Length - 1);
        uiController.HideAll();
        SetupLevel();
    }
    private void OnPlayerMove(bool success, Vector2Int dir)
    {
        if (success)
        {
            player.Inputs.Disable();
            enemy.Move();
        }
    }
    private void OnEnemyEndedTurn()
    {
        if(enemy.GetCurrentPos == player.GetCurrentPos)
        {
            //Player Lost
            uiController.ShowDefeat();
            currentState = GameStates.Lost;
        }
        else
        {
            if(player.GetCurrentPos == levels[currentLevel].ExitPos)
            {
                uiController.ShowVictory(levels[currentLevel].Message);
                currentState = GameStates.Won;
            }
            else
            {
                player.Inputs.Enable();
            }
        }
    }

    private void Continue()
    {
        if(currentState == GameStates.Won)
        {
            NextLevel();
        }
        else if(currentState == GameStates.Lost)
        {
            SetupLevel();
        }
    }
}

public enum GameStates
{
    Playing,
    Lost,
    Won
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    //Game Components
    [SerializeField] PlayerController player;
    [SerializeField] EnemyController enemy;
    //Exit Sprite
    [SerializeField] Transform exit;
    //Reference to both the gridController and it's levels
    [SerializeField] GridController grid;
    [SerializeField] GridObject[] levels;
    //Reference to the simple ui controller
    [SerializeField] UiController uiController;
    GameStates currentState = GameStates.Playing;

    private int currentLevel = 0;
    //Initialize most components
    private void Awake()
    {
        grid.SetCurrentGrid(levels[currentLevel]);
        //Player initialization
        player.INIT(grid);
        player.SetEvents(enemy.Move);
        player.onMove += OnPlayerMove;
        //Enemy Initialization
        enemy.INIT(grid);
        enemy.SetTarget(player);
        enemy.onTurnFinished += OnEnemyEndedTurn;
        //Level initialization
        SetupLevel();
    }

    private void Update()
    {
        //Note: This GetKeyDowns are here and not on the
        //inputs class, since the inputs class deals with the player and not other 
        //things in the game, like state management
        //So that's why this is here
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
        grid.SetCurrentGrid(levels[currentLevel]);
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
    //Listening to when player moves unsuccesfully or not
    private void OnPlayerMove(bool success, Vector2Int dir)
    {
        if (success)
        {
            if (player.GetCurrentPos == levels[currentLevel].ExitPos)
            {
                uiController.ShowVictory(levels[currentLevel].Message);
                currentState = GameStates.Won;
            }
            else
            {
                enemy.Move();
                //player.Inputs.Enable();
            }

            player.Inputs.Disable();
            //if(currentState != GameStates.Playing)
            //{

            //enemy.Move();
            //}
        }
    }
    //Called when enemy has no more moves, allowing the player to move again
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
            player.Inputs.Enable();
            //if (player.GetCurrentPos == levels[currentLevel].ExitPos)
            //{
            //    //uiController.ShowVictory(levels[currentLevel].Message);
            //    //currentState = GameStates.Won;
            //}
            //else
            //{
            //    player.Inputs.Enable();
            //}
        }
    }
    //In case of victory or defeat continue the game
    private void Continue()
    {
        if(currentState == GameStates.Won)
        {
            NextLevel();
            currentState = GameStates.Playing;
        }
        else if(currentState == GameStates.Lost)
        {
            SetupLevel();
            currentState = GameStates.Playing;
        }
    }
}
//States of the gameManager
public enum GameStates
{
    Playing,
    Lost,
    Won
}
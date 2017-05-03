using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour, ICameraUser {

    public Application app;
    public SnakeView snakeV;

    private SnakeDirection curDirection;
         
	// Use this for initialization
	void Start () {
        curDirection = SnakeDirection.RIGHT;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            curDirection = SnakeDirection.LEFT;
            snakeV.CurTick = snakeV.MaxTicks;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            curDirection = SnakeDirection.RIGHT;
            snakeV.CurTick = snakeV.MaxTicks;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            curDirection = SnakeDirection.UP;
            snakeV.CurTick = snakeV.MaxTicks;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            curDirection = SnakeDirection.DOWN;
            snakeV.CurTick = snakeV.MaxTicks;
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitAttempt();
        }
        SetSnakeDirection(curDirection);
        gameObject.transform.position = snakeV.transform.position;
    }

    public void LoadLevel(int level)
    {
        snakeV.snakeM.CurrentLevel = level;
        if (!LoadLevelGround())
        {
            SetVisibility(false);
            //todo error msg
            app.pickLevelCtrl.setVisibility(true);
            return;
        }
        SetSnakeDirection(curDirection);
        snakeV.CreateSnake();
        snakeV.snakeM.SnakeBody.First.Value.GetComponent<SnakeHeadController>().App = app;
        GenerateIncrementer();
        snakeV.snakeM.LevelWinScore = level * SnakeModel.WIN_MUL;
    }

    public void EnableCamera()
    {
        foreach (ICameraUser cameraUser in app.GetCameraUsers())
        {
            cameraUser.DisableCamera();
        }
        snakeV.snakeM.camera.enabled = true;
        snakeV.snakeM.camera.gameObject.SetActive(true);
    }

    public void DisableCamera()
    {
        snakeV.snakeM.camera.gameObject.SetActive(false);
        snakeV.snakeM.camera.enabled = false;
    }

    public void SetVisibility(bool visibility)
    {
        if (visibility)
        {
            EnableCamera();
        }
        else
        {
            DisableCamera();
        }
    }

    private void GenerateGround(float x, float y, int width, int length)
    {
        snakeV.GenerateGround(x, y, width, length);
    }

    private void SetSnakeDirection(SnakeDirection dir)
    {
        snakeV.SetSnakeDirection(dir);
    }

    public void IncreaseSnakeSize()
    {
        snakeV.AddSnakeLength(snakeV.snakeM.Incrementer);
        snakeV.snakeM.CurrentScore += snakeV.snakeM.Incrementer;
        GenerateIncrementer();
        if (IsWin())
        {
            Win();
        }
    }

    public void Lose()
    {
        //TODO lose msg
        ExitFromLevel();
    }

    public void Win()
    {
        //TODO win msg
        app.pickLevelCtrl.SetLevelCompleted(snakeV.snakeM.CurrentLevel);
        ExitFromLevel();
    }

    public void ExitAttempt()
    {
        //TODO
        ExitFromLevel(false);
    }

    private void ExitFromLevel()
    {
        ExitFromLevel(true);
    }

    private void ExitFromLevel(bool saveHighScore)
    {
        snakeV.DestroySnake();
        if (saveHighScore)
        {
            app.pickLevelCtrl.SetLevelNewHighScore(snakeV.snakeM.CurrentLevel, snakeV.snakeM.CurrentScore);
        }
        snakeV.snakeM.CurrentScore = 0;
        app.pickLevelCtrl.ReloadLevelsData();
        SetVisibility(false);
        app.pickLevelCtrl.setVisibility(true);
    }

    public bool IsWin()
    {
        return ((snakeV.snakeM.LevelWinScore - snakeV.snakeM.CurrentScore) < 0);
    }

    public bool LoadLevelGround()
    {
        snakeV.snakeM.LevelMap = DataManipulator.GetInstance().GetLevel(snakeV.snakeM.CurrentLevel);
        if (snakeV.snakeM.LevelMap.Count == 0)
        {
            return false;
        }
        int width = snakeV.snakeM.GetLevelWidth();
        int len = snakeV.snakeM.GetLevelLength();
        GenerateGround(SnakeModel.GROUND_X, SnakeModel.GROUND_Z, width, len);
        int i = 0;
        foreach (string cur in snakeV.snakeM.LevelMap)
        {
            for (int j = 0; j < cur.Length; j++)
            {
                switch (cur[j])
                {
                    case SnakeModel.EMPTY_SPACE:

                        break;
                    default:
                        CreateWall(cur.Length - j, snakeV.snakeM.GetLevelLength() - i);
                        break;
                }
            }
            i++;
        }
        return true;
    }

    private void CreateWall(int x, int z)
    {
        snakeV.CreateWall(x,z);
    }

    private void GenerateIncrementer()
    {
        snakeV.GenerateIncrementer();
        snakeV.snakeM.CurrentIncrementerGameObject.GetComponent<IncrementerController>().App = app;
    }

}

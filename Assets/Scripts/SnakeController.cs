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

    public void loadLevel(int level)
    {
        snakeV.snakeM.CurrentLevel = level;
        LoadLevelGround();
        SetSnakeDirection(curDirection);
        snakeV.CreateSnake();
        snakeV.snakeM.SnakeBody.First.Value.GetComponent<SnakeHeadController>().App = app;
        snakeV.GenerateIncrementer();
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
        snakeV.GenerateIncrementer();
        if (IsWin())
        {
            Win();
        }
    }

    public void Lose()
    {
        //TODO
        ExitFromLevel();
    }

    public void Win()
    {
        //TODO
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

    public void LoadLevelGround()
    {
        GenerateGround(-10, -10, 200, 200);
    }

}

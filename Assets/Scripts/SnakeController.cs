using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour, ICameraUser {

    public ApplicationSnake app;
    public SnakeView snakeV;

    private SnakeDirection curDirection;
    private bool isLastDialogAccepted;
    private delegate void ContinueMethod();
    private ContinueMethod currentContinueMethod;
    private bool isActive;
    
    void Start () {
        curDirection = SnakeDirection.NONE;
        isActive = false;
    }
	
	void Update () {
        if (!isActive)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (curDirection != SnakeDirection.RIGHT)
            {
                curDirection = SnakeDirection.LEFT;
                snakeV.CurTick = snakeV.MaxTicks;
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(curDirection != SnakeDirection.LEFT)
            {
                curDirection = SnakeDirection.RIGHT;
                snakeV.CurTick = snakeV.MaxTicks;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (curDirection != SnakeDirection.DOWN)
            {
                curDirection = SnakeDirection.UP;
                snakeV.CurTick = snakeV.MaxTicks;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (curDirection != SnakeDirection.UP)
            {
                curDirection = SnakeDirection.DOWN;
                snakeV.CurTick = snakeV.MaxTicks;
            }
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
        curDirection = SnakeDirection.NONE;
        snakeV.snakeM.CurrentLevel = level;
        if (!LoadLevelGround())
        {
            ShowDialogBox(SnakeModel.MSG_THERE_IS_NO_SUCH_LEVEL, false,LoadLevelContinueAfterError);
            return;
        }
        SetSnakeDirection(curDirection);
        snakeV.CreateSnake();
        snakeV.snakeM.SnakeBody.First.Value.GetComponent<SnakeHeadController>().App = app;
        GenerateIncrementer();
        snakeV.snakeM.LevelWinScore = level * SnakeModel.WIN_MUL;
    }

    private void LoadLevelContinueAfterError()
    {
        SetVisibility(false);
        app.pickLevelCtrl.setVisibility(true);
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
        isActive = visibility;
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
        snakeV.snakeM.increaseSnakeSource.Play();
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
        FreezeGame(true);
        snakeV.snakeM.explosionSource.Play();
        ShowDialogBox(SnakeModel.MSG_LOSE, false, LoseContinueMethod);
    }

    private void LoseContinueMethod()
    {
        ExitFromLevel();
    }

    public void Win()
    {
        FreezeGame(true);
        snakeV.snakeM.winSource.Play();
        ShowDialogBox(SnakeModel.MSG_WIN, false, WinContinueMethod);
    }

    public void WinContinueMethod()
    {
        app.pickLevelCtrl.SetLevelCompleted(snakeV.snakeM.CurrentLevel);
        ExitFromLevel();
    }

    public void ExitAttempt()
    {
        FreezeGame(true);
        ShowDialogBox(SnakeModel.MSG_DO_YOU_WANT_LEAVE,true,ExitAttemptContinueMethod);
        
    }

    private void ExitAttemptContinueMethod()
    {
        if (isLastDialogAccepted)
        {
            ExitFromLevel(false);
        }
        else
        {
            FreezeGame(false);
        }
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

    private void FreezeGame(bool freezeStatus)
    {
        snakeV.snakeM.IsGameFrozen = freezeStatus;
    }

    private void DialogBoxOkClicked()
    {
        isLastDialogAccepted = true;
        HideDialogBox();
    }

    private void DialogBoxCancelClicked()
    {
        isLastDialogAccepted = false;
        HideDialogBox();
    }

    private void HideDialogBox()
    {
        app.dialogCtrl.SetVisibility(false);
        if (currentContinueMethod != null)
        {
            currentContinueMethod();
        }
    }

    private void ShowDialogBox(string msg, bool showCancel, ContinueMethod contMeth)
    {
        currentContinueMethod = contMeth;
        app.dialogCtrl.SetVisibilityButtonOk(true);
        app.dialogCtrl.okClickImpl = DialogBoxOkClicked;
        if (showCancel)
        {
            app.dialogCtrl.cancelClickImpl = DialogBoxCancelClicked;
        }
        app.dialogCtrl.SetVisibilityButtonCancel(showCancel);
        app.dialogCtrl.SetText(msg);
        app.dialogCtrl.SetVisibility(true);
    }

}

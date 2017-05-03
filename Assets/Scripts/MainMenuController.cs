using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour, ICameraUser
{

    public ApplicationSnake app;
    public MainMenuView mainMenuV;

    private bool isLastDialogAccepted;
    private delegate void ContinueMethod();
    private ContinueMethod currentContinueMethod;

    void Start () {

    }
	
	void Update () {
		
	}

    public void SetVisibility(bool visibility)
    {
        mainMenuV.mainMenuModel.mainCanvas.gameObject.SetActive(visibility);
        if (visibility)
        {
            EnableCamera();
        } else
        {
            DisableCamera();
        }
    }

    public void onBtnPlayClick()
    {
        SetVisibility(false);
        app.pickLevelCtrl.setVisibility(true);
    }

    public void OnBtnResetClick()
    {
        ShowDialogBox(MainMenuModel.MSG_DO_YOU_WANT_RESET, true, OnBtnResetClickContinue);
    }

    private void OnBtnResetClickContinue()
    {
        if (isLastDialogAccepted)
        {
            app.pickLevelCtrl.DeleteAllSaves();
        }
    }

    public void EnableCamera()
    {
        foreach (ICameraUser cameraUser in app.GetCameraUsers())
        {
            cameraUser.DisableCamera();
        }
        mainMenuV.mainMenuModel.camera.enabled = true;
        mainMenuV.mainMenuModel.camera.gameObject.SetActive(true);
    }

    public void DisableCamera()
    {
        mainMenuV.mainMenuModel.camera.gameObject.SetActive(false);
        mainMenuV.mainMenuModel.camera.enabled = false;
    }

    public void OnExitClicked()
    {
        Application.Quit();
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

    private void HideDialogBox()
    {
        app.dialogCtrl.SetVisibility(false);
        if (currentContinueMethod != null)
        {
            currentContinueMethod();
        }
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
    
}

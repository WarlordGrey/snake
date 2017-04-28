using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour, ICameraUser
{

    public Application app;
    public MainMenuView mainMenuV;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setVisibility(bool visibility)
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
        setVisibility(false);
        app.pickLevelCtrl.setVisibility(true);
    }

    public void onBtnResetClick()
    {

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

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour {

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
    }

    public void onBtnPlayClick()
    {
        setVisibility(false);
        app.pickLevelCtrl.setVisibility(true);
    }

    public void onBtnResetClick()
    {

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Application : MonoBehaviour {

    public MainMenuController mainMenuCtrl;
    public PickLevelController pickLevelCtrl;
    public SnakeController snakeCtrl;
    public DialogBoxController dialogCtrl;

    private List<ICameraUser> camUsers = null;

    // Use this for initialization
    void Start ()
    {
        mainMenuCtrl.setVisibility(true);
        pickLevelCtrl.setVisibility(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    internal List<ICameraUser> GetCameraUsers()
    {
        if(camUsers == null)
        {
            camUsers = new List<ICameraUser>();
            camUsers.Add(mainMenuCtrl);
            camUsers.Add(pickLevelCtrl);
            camUsers.Add(snakeCtrl);
        }
        return camUsers;
    }

}

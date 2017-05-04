using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationSnake : MonoBehaviour {

    public MainMenuController mainMenuCtrl;
    public PickLevelController pickLevelCtrl;
    public SnakeController snakeCtrl;
    public DialogBoxController dialogCtrl;
    public AudioSource mainTheme;

    private List<ICameraUser> camUsers = null;

    void Start ()
    {
        mainMenuCtrl.SetVisibility(true);
        pickLevelCtrl.setVisibility(false);
        IsMainThemeOn = true;
        if (IsMainThemeOn)
        {
            mainTheme.Play();
        }
    }
	
	void Update () {
		
	}

    public bool IsMainThemeOn { get; set; }

    public List<ICameraUser> GetCameraUsers()
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

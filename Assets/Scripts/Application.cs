using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Application : MonoBehaviour {

    public MainMenuController mainMenuCtrl;
    public PickLevelController pickLevelCtrl;
    public SnakeController snakeCtrl;

	// Use this for initialization
	void Start () {
        mainMenuCtrl.setVisibility(true);
        pickLevelCtrl.setVisibility(false);
        pickLevelCtrl.pickLevelV.pickLevelM.LevelsCount = 40;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    

}

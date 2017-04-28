using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour, ICameraUser {

    public Application app;
    public SnakeView snakeV;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void loadLevel(int level)
    {
        Debug.Log("Picked level = " + level);
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

}

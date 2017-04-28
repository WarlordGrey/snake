using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PickLevelController : MonoBehaviour, ICameraUser
{

    public Application app;
    public PickLevelView pickLevelV;

    private int curPage;

    // Use this for initialization
    void Start () {
        curPage = 1;
        InitLevels();
    }
	
	// Update is called once per frame
	void Update () {

	}

    public void setVisibility(bool visibility)
    {
        pickLevelV.pickLevelM.pickLevelCanvas.gameObject.SetActive(visibility);
        if (visibility)
        {
            EnableCamera();
        }
        else
        {
            DisableCamera();
        }
    }

    private void OnBtnPickLevelClick()
    {
        app.snakeCtrl.loadLevel(EventSystem.current.currentSelectedGameObject.GetComponent<ButtonLevelData>().Lvl);
        setVisibility(false);
        app.snakeCtrl.SetVisibility(true);
    }

    public void OnReturnToMenuClick()
    {
        setVisibility(false);
        app.mainMenuCtrl.setVisibility(true);
    }

    public void OnNextPageClick()
    {
        PickLevelModel plm = pickLevelV.pickLevelM;
        if(curPage < (plm.LevelsCount / plm.GetMaxLevelsPerPage()))
        {
            curPage++;
            pickLevelV.RenderLvlButtons(curPage);
        }
    }

    public void OnPrevPageClick()
    {
        if (curPage > 1)
        {
            curPage--;
            pickLevelV.RenderLvlButtons(curPage);
        }
    }

    private void InitLevels()
    {
        while (pickLevelV.pickLevelM.LevelsButtons == null);
        while (pickLevelV.pickLevelM.LevelsButtons.Count == 0);
        foreach (Button cur in pickLevelV.pickLevelM.LevelsButtons)
        {
            
            cur.onClick.AddListener(() => OnBtnPickLevelClick());
        }
        LoadLevelsData();
    }

    private void LoadLevelsData()
    {
        PickLevelData lvlsData = DataManipulator.GetInstance().DeSerializeObject<PickLevelData>(PickLevelModel.DATA_FILENAME);
        if(lvlsData == null)
        {
            lvlsData = CreateNewLevelsData();
            CreateNewLevelsData();
            SaveLevelsData(lvlsData);
        }
        pickLevelV.pickLevelM.SetLevelsData(lvlsData);
        pickLevelV.RenderLvlButtons(curPage);
    }

    private PickLevelData CreateNewLevelsData()
    {
        PickLevelData lvlsData = new PickLevelData();
        List<int> highScores = new List<int>();
        for (int i = 0; i < pickLevelV.pickLevelM.LevelsCount; i++)
        {
            highScores.Add(0);
        }
        lvlsData.HighScores = highScores;
        return lvlsData;
    }

    public void SaveLevelsData(PickLevelData lvlsData)
    {
        DataManipulator.GetInstance().SerializeObject<PickLevelData>(lvlsData, PickLevelModel.DATA_FILENAME);
    }

    public void SetLevelNewHighScore(int level, int score)
    {
        pickLevelV.pickLevelM.SetLevelsHighScore(level, score);
    }

    public void ReloadLevelsData()
    {
        LoadLevelsData();
    }

    public void EnableCamera()
    {
        foreach(ICameraUser cameraUser in app.GetCameraUsers())
        {
            cameraUser.DisableCamera();
        }
        pickLevelV.pickLevelM.camera.enabled = true;
        pickLevelV.pickLevelM.camera.gameObject.SetActive(true);
    }

    public void DisableCamera()
    {
        pickLevelV.pickLevelM.camera.gameObject.SetActive(false);
        pickLevelV.pickLevelM.camera.enabled = false;
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickLevelModel : MonoBehaviour
{

    public const string LOCK_NAME_COUNT_LEVELS = "PickLevelSyncLocker";
    public const int LEVELS_PER_ROW = 4;
    public const int LEVELS_PER_COLUMN = 5;

    public Camera camera;
    public Light light;
    public Canvas pickLevelCanvas;
    public Button typicalPickLevelButton;

    private int lvlCnt;
    private bool isLvlCntChanged;

    public List<Button> LevelsButtons { get; set; }
    public int LevelsCount {
        get
        {
            return lvlCnt;
        }
        set {
            lvlCnt = value;
            IsLevelsCountChanged = true;
        }
    }
    public bool IsLevelsCountChanged {
        get { lock (PickLevelModel.LOCK_NAME_COUNT_LEVELS) { return isLvlCntChanged; } }
        set { lock (PickLevelModel.LOCK_NAME_COUNT_LEVELS) { isLvlCntChanged = value; } }
    }

    // Use this for initialization
    void Start () {
        IsLevelsCountChanged = false;
        InitLevelsButtons();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void InitLevelsButtons()
    {
        LevelsButtons = new List<Button>();
        int btnCnt = GetMaxLevelsPerPage();
        for (int i = 0; i < btnCnt; i++)
        {
            Button newLvl = Button.Instantiate(typicalPickLevelButton);
            newLvl.transform.SetParent(pickLevelCanvas.gameObject.transform);
            LevelsButtons.Add(newLvl);
        }
    }

    internal string GetLevelDescription(int curLevel)
    {
        return curLevel.ToString();
    }

    public int GetMaxLevelsPerPage()
    {
        return LEVELS_PER_COLUMN * LEVELS_PER_ROW;
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickLevelModel : MonoBehaviour
{

    public const string DATA_FILENAME = "Assets/Levels/lvls.data";
    public const int LEVELS_PER_ROW = 4;
    public const int LEVELS_PER_COLUMN = 5;

    public Camera camera;
    public Light light;
    public Canvas pickLevelCanvas;
    public Button typicalPickLevelButton;

    private int lvlCnt = 20;
    private PickLevelData lvlsData;

    public List<Button> LevelsButtons { get; set; }
    public int LevelsCount {
        get
        {
            return lvlCnt;
        }
        set {
            lvlCnt = value;
        }
    }

    // Use this for initialization
    void Start () {
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
            newLvl.gameObject.AddComponent<ButtonLevelData>();
            newLvl.gameObject.GetComponent<ButtonLevelData>().Lvl = i + 1;
            LevelsButtons.Add(newLvl);
        }
    }

    internal string GetLevelDescription(int level)
    {
        return level.ToString() + "\n-----\n" + GetLevelsHighScore(level).ToString();
    }

    public int GetMaxLevelsPerPage()
    {
        return LEVELS_PER_COLUMN * LEVELS_PER_ROW;
    }

    public bool IsLevelUnlocked(int level)
    {
        if (level == 1)
        {
            return true;
        }
        return IsLevelCompleted(level - 1);
    }

    public void SetLevelsData(PickLevelData lvlsData)
    {
        this.lvlsData = lvlsData;
    }

    public PickLevelData GetLevelsData()
    {
        return lvlsData;
    }

    public int GetLevelsHighScore(int level)
    {
        int highScore = 0;
        if (lvlsData.AllLevels.Count > level)
        {
            highScore = lvlsData.AllLevels[(level - 1)].HighScore;
        }
        return highScore;
    }

    public void SetLevelsHighScore(int level, int score)
    {
        if (lvlsData.AllLevels.Count <= level)
        {
            return;
        }
        if (score > GetLevelsHighScore(level))
        {
            lvlsData.AllLevels[(level - 1)].HighScore = score;
        }
    }

    public void SetLevelCompleted(int level)
    {
        if (lvlsData.AllLevels.Count <= level)
        {
            return;
        }
        lvlsData.AllLevels[(level - 1)].IsCompleted = true;
    }

    public bool IsLevelCompleted(int level)
    {
        if (lvlsData.AllLevels.Count <= level)
        {
            return false;
        }
        return lvlsData.AllLevels[(level - 1)].IsCompleted;
    }

}

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
    
    void Start () {
        
    }
	
	void Update () {
		
	}

    public string GetLevelDescription(int level)
    {
        String retVal = level.ToString();
        if(GetLevelsHighScore(level) > 0)
        {
            retVal += "\n-----\n" + GetLevelsHighScore(level).ToString();
        }
        return retVal;
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[System.Serializable]
public class SingleLevelData
{

    private int hs;
    private bool compl;

    public SingleLevelData(int highscore, bool isCompleted)
    {
        HighScore = highscore;
        IsCompleted = isCompleted;
    }

    public SingleLevelData(int highscore)
    {
        HighScore = highscore;
        IsCompleted = false;
    }

    public SingleLevelData()
    {
        HighScore = 0;
        IsCompleted = false;
    }

    public int HighScore { get { return hs; } set { hs = value; } }

    public bool IsCompleted { get { return compl; } set { compl = value; } }

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FileData
{
    private int unlockedLevelNum = 1;
    private IDictionary<int, int> starsInLevels = new Dictionary<int, int>();

    public FileData()
    {
        for(int i = 1; i < GameData.GetLevelsCount(); i++)
        {
            starsInLevels[i] = 0;
        }
    }

    public IDictionary<int, int> GetStarsInLevels()
    {
        return starsInLevels;
    }

    public void SetStarsInLevel(int level, int stars)
    {
        starsInLevels[level] = stars;
    }

    public int UnlockedLevelNum
    {
        get => unlockedLevelNum;
        set
        {
            if (value > unlockedLevelNum)
                unlockedLevelNum = value;
        }
    }
}

using UnityEngine;
using System.Collections;

public static class ScriptLevelRatings {

    public static int[] Ratings { get; private set; }

    public static int currentLevel;

    public static void UpdateRating(int levelNumber, int rating)
    {
        if(levelNumber >= Ratings.Length && rating > Ratings[levelNumber])
        {
            Ratings[levelNumber] = rating;
        }
    }

    public static void LoadData()
    {
        Ratings = new int[6];
        for (int i = 0; i < Ratings.Length; i++)
        {
            Ratings[i] = PlayerPrefs.GetInt("Rating" + i);
        }
    }

    public static void SaveData()
    {
        for (int i = 0; i < Ratings.Length; i++)
        {
            PlayerPrefs.SetInt("Rating" + i, Ratings[i]);
        }
    }

    public static void Initialize()
    {
        Ratings = new int[6];
        for (int i = 0; i < Ratings.Length; i++)
        {
            Ratings[i] = 0;
        }
    }

    public static int GetCurrentLevelRating()
    {
        return Ratings[currentLevel];
    }
}

using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class RankData
{
    public const int SIZE = 5;
    public int[] score;
    public System.DateTime[] date;

    public RankData()
    {
        score = new int[SIZE];
        date = new System.DateTime[SIZE];
        for(int i=0; i<SIZE; i++)
        {
            score[i] = 0;
            date[i] = System.DateTime.MinValue;
        }
    }
}


public static class GameStats
{
    public static int gameoverType;

    public static int latestScore = 0;
    public static float latestTime = 0;


    public static string path = Application.persistentDataPath + "/Data.bin";
    public static RankData rankData;


    public static void UpdateRank()
    {
        int LEN = rankData.score.Length;
        for (int i = 0; i < LEN; i++)
        {
            if(rankData.score[i] < latestScore)
            {
                // 한 칸씩 뒤로 미뤄 자리를 만듬
                for (int j = LEN - 1; j > i; j--)
                {
                    rankData.score[j] = rankData.score[j - 1];
                    rankData.date[j] = rankData.date[j - 1];
                }

                rankData.score[i] = latestScore;
                rankData.date[i] = System.DateTime.Now;
                break;
            }
        }
    }

    public static void SaveRank()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, rankData);
        stream.Close();
    }

    public static void LoadRank()
    {
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            rankData = formatter.Deserialize(stream) as RankData;
            stream.Close();

        }
        else
        {
            Debug.LogError("No Save File in " + path + ", Creating empty file");

            rankData = new RankData();
        }
    }
}

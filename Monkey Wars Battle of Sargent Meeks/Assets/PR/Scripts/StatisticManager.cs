using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public static class StatisticManager
{
    [System.Serializable]
    public class Stat
    {
        public string name;
        public string desc;
        public float amount;

        public void Set(Stat s)
        {
            name = s.name;
            desc = s.desc;
            amount = s.amount;
        }
    }

    [System.Serializable]
    public class StatList
    {
        public Stat[] stats;
    }

    public static StatList statList;
    private static StatList prevStats;
    private static string jsonFile;

    private static string path = Application.streamingAssetsPath + "/stats.json"; 

    public static void LoadData()
    {
        //TextAsset jFile = (TextAsset)Resources.Load("stats");
        string jsonFile = File.ReadAllText(path);

        statList = JsonUtility.FromJson<StatList>(jsonFile);
    }

    public static void WriteData()
    {
        string output = JsonUtility.ToJson(statList);

        File.WriteAllText(path, output);
    }

    public static int FindStatByName(string name)
    {
        for(int i = 0; i < statList.stats.Length; i++)
        {
            if (statList.stats[i].name == name)
            {
                return i;
            }
        }

        return -1;
    }

    public static void UpdateStat(string name, float a)
    {
        try
        {
            statList.stats[FindStatByName(name)].amount += a;
        }
        catch
        {
            Debug.Log("Statistic could not be found, cannot update");
        }
    }

    public static void ClearStats()
    {
        for (int i = 0; i < statList.stats.Length; i++)
        {
            statList.stats[i].amount = 0;
        }

        WriteData();
    }

    public static void LoadPrev()
    {
        if (prevStats == null)
        {
            prevStats = new StatList();
            prevStats.stats = new Stat[statList.stats.Length];

            for (int i = 0; i < prevStats.stats.Length; i++)
            {
                prevStats.stats[i] = new Stat();
            }
        }

        for (int i = 0; i < prevStats.stats.Length; i++)
        {
            prevStats.stats[i].Set(statList.stats[i]);
        }
    }

    public static float FindDifference(string name)
    {
        float v;
        int i = FindStatByName(name);

        v = statList.stats[i].amount - prevStats.stats[i].amount;
        return v;
    }
}

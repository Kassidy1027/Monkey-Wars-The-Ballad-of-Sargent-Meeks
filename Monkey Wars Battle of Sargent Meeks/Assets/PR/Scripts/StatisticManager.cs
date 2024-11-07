using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class StatisticManager
{
    [System.Serializable]
    public class Stat
    {
        public string name;
        public string desc;
        public float amount;
    }

    [System.Serializable]
    public class StatList
    {
        public Stat[] stats;
    }

    public static StatList statList;
    private static TextAsset jsonFile;


    public static void LoadData()
    {
        jsonFile = (TextAsset)Resources.Load("stats");

        statList = JsonUtility.FromJson<StatList>(jsonFile.text);

    }

    public static void WriteData()
    {
        string output = JsonUtility.ToJson(statList);

        File.WriteAllText(Application.dataPath + "/Resources/stats.json", output);
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
    }
}

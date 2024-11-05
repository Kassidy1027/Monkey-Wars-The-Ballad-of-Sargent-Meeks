using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.InteropServices.WindowsRuntime;

public class StatisticDisplayHandler : MonoBehaviour
{
    public TMP_Text[] texts;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            float i = StatisticManager.statList.stats[0].amount;
        }
        catch
        {
            StatisticManager.LoadData();
        }
        finally
        {
            UpdateDisplayStats();
        }
    }

    string PrintStat(string name)
    {
        int i = StatisticManager.FindStatByName(name);

        string text = string.Format(StatisticManager.statList.stats[i].desc, StatisticManager.statList.stats[i].amount);
        return text;
    }

    public void UpdateDisplayStats()
    {
        foreach (TMP_Text t in texts)
        {
            t.text = PrintStat(t.gameObject.name);
        }
    }
}

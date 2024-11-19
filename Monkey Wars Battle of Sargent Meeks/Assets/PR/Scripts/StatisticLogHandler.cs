using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatisticLogHandler : MonoBehaviour
{
    public TMP_Text[] texts;
    public GameObject panel;
    public static StatisticLogHandler SL;

    private void Awake()
    {
        if (StatisticLogHandler.SL == null)
        {
            StatisticLogHandler.SL = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void WriteGameStats()
    {
        panel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;

        foreach(TMP_Text t in texts)
        {
            int i = StatisticManager.FindStatByName(t.gameObject.name);
            float v = StatisticManager.FindDifference(t.gameObject.name);
            t.text = string.Format(StatisticManager.statList.stats[i].desc, v);
        }

        StatisticManager.WriteData();
    }


}

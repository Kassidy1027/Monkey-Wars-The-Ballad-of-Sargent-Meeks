using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITextController : MonoBehaviour
{
    public TMP_Text roundText;
    public TMP_Text enemyText;
    public TMP_Text pointsText;

    public void UpdateRoundText(int r)
    {
        roundText.text = "Round: " + r.ToString();
    }

    public void UpdateEnemyCount(int en)
    {
        enemyText.text = "Enemies: " + en.ToString();
    }

    public void UpdatePointCount(int p)
    {
        pointsText.text = "Points: " + p.ToString();
    }
}

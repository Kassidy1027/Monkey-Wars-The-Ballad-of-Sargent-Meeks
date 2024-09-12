using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITextController : MonoBehaviour
{
    public TMP_Text roundText;
    public TMP_Text enemyText;

    public void UpdateText(int en, int r)
    {
        roundText.text = "Round: " + r.ToString();
        enemyText.text = "Enemies: " + en.ToString();
    }
}

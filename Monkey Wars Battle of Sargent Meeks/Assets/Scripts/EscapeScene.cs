using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeScene : MonoBehaviour
{
    public bool escaped = false;
    public int timer1 = 0;
    public int timer2 = 0;
    void Update()
    {
        if (escaped && timer1 <= 200)
        {
            this.transform.Translate(0f, 0.1f, 0f);
            timer1 += 1;
        }
        else if (escaped && timer1 >= 200)
        {
            this.transform.Translate(0f, 0f, 0.2f);
        }
    }
}

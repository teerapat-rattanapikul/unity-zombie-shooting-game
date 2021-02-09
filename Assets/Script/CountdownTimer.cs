using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public float countdownTime = 120; // seconds

    float remainTime = 0;

    public bool startCount;
    public bool finishCount;

    public Text timeText;

    private void Update()
    {
        if (startCount)
        {
            if (remainTime > 0f)
            {
                remainTime -= Time.deltaTime;
                timeText.text = "Time Remain: " + Mathf.FloorToInt(remainTime).ToString();
            }
            else if (!finishCount)
            {
                remainTime = -1f;
                timeText.text = "Go To Next Scene!!";
                finishCount = true;

                // clear level 1
                GameManager.Instance.ClearLevel();
            }
        }
    }

    public void ResetTimer()
    {
        remainTime = countdownTime;
        startCount = false;
        finishCount = false;
        timeText.text = "Time Remain: " + remainTime.ToString();
    }
}

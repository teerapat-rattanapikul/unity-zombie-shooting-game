using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameUI : MonoBehaviour
{
    public Text gameResult;
    public Text gameScore;

    public void LoseGame()
    {

        gameObject.SetActive(true);
        gameResult.text = "You Died!!";
        gameScore.text = "Your Score: " + GameManager.Instance.score;
        Time.timeScale = 0;

    }

    public void WinGame()
    {
        gameObject.SetActive(true);
        gameResult.text = "You Win!";
        gameScore.text = "Your Score: " + GameManager.Instance.score;
        Time.timeScale = 0;
    }

    private void Update()
    {
        if (gameObject.activeInHierarchy && Input.GetKeyDown(KeyCode.N))
        {
            Time.timeScale = 1;
            GameManager.Instance.ResetGame();
        }
    }

}

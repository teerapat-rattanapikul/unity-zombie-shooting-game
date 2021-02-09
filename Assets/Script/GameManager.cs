using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public AudioClip titleSound;
    public AudioClip clicked;

    public AudioClip endSound;
    AudioSource sound;
    private static GameManager m_instance;
    public static GameManager Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<GameManager>();
                if (m_instance == null)
                {
                    Debug.LogWarning(typeof(GameManager) + "not found.");
                }
            }
            return m_instance;
        }
    }

    public Slider healthBar;
    public Text scoreText;

    public int score;
    public GameObject playerInstance;

    public CountdownTimer timer;

    public List<GameObject> enemyInScene = new List<GameObject>();
    public int limitEnemyInScene = 50;

    public GameObject menuUI;
    public GameObject gameplayUI;
    public EndGameUI endGameUI;

    public ammoUi AmmoUi;

    // flash red when player take damage
    public FlashRed flashRed;

    public enum GameState
    {
        SCENE1,
        BOSS,
        MENU,
    }

    public GameState state = GameState.MENU;

    private void Awake()
    {
        if (this != Instance)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        sound = GetComponent<AudioSource>();
        sound.PlayOneShot(titleSound);
        gameplayUI.SetActive(false);
        menuUI.SetActive(true);
        endGameUI.gameObject.SetActive(false);
        // StartLevel();

        // unlock mouse
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ClickStartGame()
    {
        state = GameState.SCENE1;
        sound.Stop();
        sound.PlayOneShot(clicked);
        SceneManager.LoadScene("Scene1");
        StartLevel();
    }

    public void ClickExit() {
        Application.Quit();
    }

    public void SetHealthBar(int currentHealth, int maxHealth)
    {
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;

        if (healthBar.value == 0)
        {
            healthBar.fillRect.gameObject.SetActive(false);
        }
    }

    public void ChangeScore(int value)
    {
        Debug.Log("plus score: " + value);
        score += value;
        scoreText.text = "Score: " + score;
    }

    public void GameEnd()
    {
        // player dead
        timer.startCount = false;
        endGameUI.LoseGame();
        sound.PlayOneShot(endSound);
 
    }

    public void StartLevel()
    {
        if (state == GameState.SCENE1)
        {

            menuUI.SetActive(false);
            gameplayUI.SetActive(true);

            // start timer when in scene1
            timer.ResetTimer();
            timer.startCount = true;
        }
        else if (state == GameState.BOSS)
        {
            // boss scene
            timer.gameObject.SetActive(false);
            enemyInScene.Clear();
            sound.PlayOneShot(endSound);
            
        }
    }

    public void ClearLevel()
    {
        // run when timelimit reach
        if (state == GameState.SCENE1)
        {
            // find all warpgate and set active to go to next level
            var objs = Resources.FindObjectsOfTypeAll(typeof(GoNextLevel)) as GoNextLevel[];
            foreach (var item in objs)
            {
                item.gameObject.SetActive(true);
            }
        }
        else if (state == GameState.BOSS)
        {
            // boss state clear
            Debug.Log("Clear game Show Score...");
            endGameUI.WinGame();
            
        }

    }

    public void SpawnEnemyInScene(GameObject enemy, Vector3 position)
    {
        if (enemyInScene.Count <= limitEnemyInScene)
        {
            var newEnemy = Instantiate(enemy, position, Quaternion.identity);
            enemyInScene.Add(newEnemy);
        }
    }

    public void RemoveEnemyInScene(GameObject enemy)
    {
        if (enemy.CompareTag("boss") && state == GameState.BOSS)
        {
            // kill boss and in boss scene
            Invoke(nameof(ClearLevel), 2.5f);
            // game end yayyy
        }
        enemyInScene.Remove(enemy);
    }

    public void ChangeToBossScene()
    {
        playerInstance.GetComponent<PlayerController>().canMove = false;
        state = GameState.BOSS;
        SceneManager.LoadScene("BossScene");
        StartLevel();
    }

    public void ResetGame()
    {
        state = GameState.MENU;

        enemyInScene.Clear();

        score = 0;
        gameplayUI.SetActive(false);
        menuUI.SetActive(true);
        endGameUI.gameObject.SetActive(false);

        Destroy(playerInstance);
        m_instance = null;
        SceneManager.LoadScene("Menu");

        Destroy(gameObject);
    }

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{

    public GameObject gameOverPanel;

    public GameUI gameUI;
    public GameObject player;
    public int score;
    public int waveCountdown;
    public bool isGameOver; // tracks whether or not the game has ended.

    // This uses the singleton pattern to have one and only one item
    // We only want one Game to track everything; score, remaining robots and current wave.
    private static Game singleton; 

    [SerializeField]
    RobotSpawn[] spawns; // spawns is the array of teleporters that spawn robots each wave

    public int enemiesLeft; //  is a counter, which tracks how many robots are still alive.

    // 1 This method frees the mouse cursor when the game is over so that the user can select something from the menu.
    public void OnGUI()
    {
        if (isGameOver && Cursor.visible == false)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
    // 2 This will be called when the game is over. It sets the timeScale to 0 so that the robots stop moving. 
    //  It also disables the controls and displays the Game Over panel 
    public void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0;
        player.GetComponent<FirstPersonController>().enabled = false;
        player.GetComponent<CharacterController>().enabled = false;
        gameOverPanel.SetActive(true);
    }
    // 3 When the user wants to restart the game, this method will be used to reload the scene to start the game again.
    public void RestartGame()
    {
        SceneManager.LoadScene(Constants.SceneBattle);
        gameOverPanel.SetActive(true);
    }
    // 4 This will be called when the user selects the Exit button. It quits the app, but only if its being run from a build.
    public void Exit()
    {
        Application.Quit();
    }
    // 5  This loads the Menu scene.
    public void MainMenu()
    {
        SceneManager.LoadScene(Constants.SceneMenu);
    }

    // 1 Initialize the singleton and call SpawnRobots().
    void Start()
    {
        singleton = this;

        StartCoroutine("increaseScoreEachSecond");
        isGameOver = false;
        Time.timeScale = 1;
        waveCountdown = 30;
        enemiesLeft = 0;
        StartCoroutine("updateWaveTimer");

        SpawnRobots();
    }
    // 2 Go through each RobotSpawn in the array and call SpawnRobot() to actually spawn a robot.
    private void SpawnRobots()
    {
        foreach (RobotSpawn spawn in spawns)
        {
            spawn.SpawnRobot();
            enemiesLeft++;
        }

        // This sets the enemy count to the latest value after spawning new robots.
        gameUI.SetEnemyText(enemiesLeft);
    }

    private IEnumerator updateWaveTimer()
    {
        while (!isGameOver)
        {
            yield return new WaitForSeconds(1f);
            waveCountdown--;
            gameUI.SetWaveText(waveCountdown);
            // Spawn next wave and restart count down
            if (waveCountdown == 0)
            {
                SpawnRobots();
                waveCountdown = 30;
                gameUI.ShowNewWaveText();
            }
        }
    }

    public static void RemoveEnemy()
    {

        singleton.enemiesLeft--;
        singleton.gameUI.SetEnemyText(singleton.enemiesLeft);
        // Give player bonus for clearing the wave before timer is done
        if (singleton.enemiesLeft == 0)
        {
            singleton.score += 50;
            singleton.gameUI.ShowWaveClearBonus();
        }
    }

    //This will give the player points when they kill a robot.
    public void AddRobotKillToScore()
    {
        score += 10;
        gameUI.SetScoreText(score);
    }

    // coroutine that updates the score every single second when the game is running
    IEnumerator increaseScoreEachSecond()
    {
        while (!isGameOver)
        {
            yield return new WaitForSeconds(1);
            score += 1;
            gameUI.SetScoreText(score);
        }
    }
}

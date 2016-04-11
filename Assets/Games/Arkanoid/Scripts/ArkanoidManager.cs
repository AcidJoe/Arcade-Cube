using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ArkanoidManager : MonoBehaviour
{
    public GameObject brick;

    public ArkanoidGrid grid;
    public ArkMap am;

    public GameObject[] briks;

    public int[,] levelMap;

    public bool isLevelCreated = false;

    public BallScript ball;
    public GameObject bat;

    public int Lives;

    public Text score;

    public GameObject live3, live2, live1;

    public Text result;

    public GameObject gameOverMenu;

    void Start()
    {
        gameOverMenu.SetActive(false);
        Lives = 3;
        Game.currentScore = 0;
    }

    void Update()
    {
        score.text = Game.currentScore.ToString();

        if (!isLevelCreated && grid.isGridReady)
        {
            CreateLevel();
        }

        if(ball.transform.position.y < -15.0f)
        {
            ball.bang.Play();
            Lives--;
            ball.isActivate = false;
        }

        if(Lives == 3)
        {
            live3.SetActive(true);
            live2.SetActive(true);
            live1.SetActive(true);
        }
        if(Lives < 3)
        {
            live3.SetActive(false);
        }
        if(Lives < 2)
        {
            live2.SetActive(false);
        }
        if (Lives <= 0)
        {
            live1.SetActive(false);
            Destroy(bat);
            GameOver();
        }

        if (isLevelCreated)
        {
            briks = GameObject.FindGameObjectsWithTag("Bullet");
        }

        if(isLevelCreated && briks.Length <= 0)
        {
            isLevelCreated = false;
        }
    }

    public void BackToMenu()
    {
        if (Game.currentScore > Game.player.record_ark)
        {
            Game.player.CheckRank((int)Game.currentScore / 10);
            Game.player.record_ark = Game.currentScore;
            SaveLoad.Save(Game.current_slot);
        }
        SceneManager.LoadScene(1);
    }

    public void GameOver()
    {
        result.text = "Ваш результат: " + Game.currentScore.ToString();
        gameOverMenu.SetActive(true);
    }

    void CreateLevel()
    {
        isLevelCreated = true;

        int ran = Random.Range(1, 3); 

        levelMap = am.map(ran);

        for (int i = 0; i <= 9; i++)
        {
            for (int j = 0; j <= 10; j++)
            {
                if (levelMap[i, j] == 1)
                {
                    foreach (ArkCell ac in grid.cells)
                    {
                        if (ac.row == i && ac.rowPos == j)
                        {
                            Instantiate(brick, ac.pos, Quaternion.identity);
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }
        }
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AsteroidManager : MonoBehaviour
{
    public Text score;
    public Text result;
    public GameObject gameOverMenu;
    public Text _live;

    public GameObject[] asteroids;

    public int lives;

    #region "Asteroids Prefabs"
    public GameObject asterbig1;
    public GameObject asterbig2;
    public GameObject asterbig3;
    public GameObject asterbig4;

    public GameObject astermid1;
    public GameObject astermid2;
    public GameObject astermid3;
    public GameObject astermid4;

    public GameObject astersm1;
    public GameObject astersm2;
    public GameObject astersm3;
    public GameObject astersm4;
    #endregion "Asteroids Prefabs"

    public GameObject spawnPoint1;
    public GameObject spawnPoint2;
    public GameObject spawnPoint3;
    public GameObject spawnPoint4;

    void Start()
    {
        lives = 3;
        gameOverMenu.SetActive(false);
        Game.currentScore = 0;
    }

    void Update()
    {
        asteroids = GameObject.FindGameObjectsWithTag("Ast");

        score.text = Game.currentScore.ToString();
        _live.text = "Жизни: " + lives.ToString();

        if(lives <= 0 && !gameOverMenu.activeInHierarchy)
        {
            GameOver();
        }

        if(asteroids.Length <= 0)
        {
            SpawnAster();
        }
    }

    void SpawnAster()
    {
        Instantiate(ranAster(), spawnPoint1.transform.position, Quaternion.identity);
        Instantiate(ranAster(), spawnPoint2.transform.position, Quaternion.identity);
        Instantiate(ranAster(), spawnPoint3.transform.position, Quaternion.identity);
        Instantiate(ranAster(), spawnPoint4.transform.position, Quaternion.identity);
    }

    GameObject ranAster()
    {
        int i = Random.Range(1, 4);

        switch (i)
        {
            case 1:
                return asterbig1;
            case 2:
                return asterbig2;
            case 3:
                return asterbig3;
            case 4:
                return asterbig4;
        }

        return asterbig1;
    }

    public void BackToMenu()
    {
        if (Game.currentScore > Game.player.record_ast)
        {
            Game.player.CheckRank((int)Game.currentScore / 10);
            Game.player.record_ast = Game.currentScore;
            SaveLoad.Save(Game.current_slot);
        }
        SceneManager.LoadScene(1);
    }

    public void GameOver()
    {
        result.text = "Ваш результат: " + Game.currentScore.ToString();
        gameOverMenu.SetActive(true);
    }
}

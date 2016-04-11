using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PongManager : MonoBehaviour
{
    public Text result;
    public GameObject gameOverMenu;
    public Text winLoseText;

    public AudioSource Goal_pl;
    public AudioSource Goal_Ai;

    public Text playerSc;
    public Text AISc;

    public GameObject ballPrefab;

    public PongBall ball;

    public AI ai;

    public int ballCount = 0;

    public int playerScore;
    public int AIScore;

    float difficulty_mod;

    int Dir = 0;

	void Start ()
    {
        Game.currentScore = 0;
        gameOverMenu.SetActive(false);

        switch (Game.current_difficulty)
        {
            case 1:
                difficulty_mod = 0.9f;
                break;
            case 2:
                difficulty_mod = 1.0f;
                break;
            case 3:
                difficulty_mod = 1.5f;
                break;
            case 4:
                difficulty_mod = 2.0f;
                break;
            case 5:
                difficulty_mod = 2.2f;
                break;
            case 6:
                difficulty_mod = 3.0f;
                break;
        }
    }
	

	void Update ()
    {
        playerSc.text = playerScore.ToString();
        AISc.text = AIScore.ToString();

        if (ballCount == 0 && !gameOverMenu.activeInHierarchy)
        {
            SpawnBall();
            ai.isBallSpawned = true;
        }

        if((playerScore == 11 || AIScore == 11) && Mathf.Abs(playerScore - AIScore) >= 2 && !gameOverMenu.activeInHierarchy)
        {
            GameOver();
        }
	}

    void SpawnBall()
    {
        ballCount += 1;
        Instantiate(ballPrefab, spawnPoint(), Quaternion.identity);

        ball = GameObject.FindGameObjectWithTag("Ball").GetComponent<PongBall>();

        if (ball)
            ball.rb.velocity = createDir(ball.transform.position, Dir) * 400.0f * difficulty_mod;
    }

    Vector2 spawnPoint()
    {
        float ran = Random.Range(-60, 60);

        return new Vector2(0, ran);
    }

    Vector2 createDir(Vector2 pos, int i)
    {
        float ran = Random.Range(-50, 50);

        if(i == 0)
        {
            while(i == 0)
            {
                i = Random.Range(-1, 1);
            }
        }

        Vector2 dir = new Vector2(i * 500, ran) - pos;

        return dir.normalized;
    }

    public void playerGoal()
    {
        Goal_Ai.Play();
        AIScore++;
        Destroy(ball.gameObject);
        ballCount -= 1;
        ai.isBallSpawned = false;
        Dir = -1;
    }

    public void AIGoal()
    {
        Goal_pl.Play();
        playerScore++;
        Destroy(ball.gameObject);
        ballCount -= 1;
        ai.isBallSpawned = false;
        Dir = 1;
    }

    public void BackToMenu()
    {
        if (Game.currentScore > Game.player.record_pong)
        {
            Game.player.CheckRank((int)Game.currentScore / 10);
            Game.player.record_pong = Game.currentScore;
            SaveLoad.Save(Game.current_slot);
        }
        SceneManager.LoadScene(1);
    }

    public void GameOver()
    {
        if (ball)
        {
            Destroy(ball);
        }
        Game.currentScore = (playerScore - AIScore) * 1000;

        if(Game.currentScore < 0)
        {
            Game.currentScore = 0;
        }

        if (AIScore - playerScore > 0)
        {
            winLoseText.text = "ВЫ ПРОИГРАЛИ !";
        }
        else
        {
            winLoseText.text = "ВЫ ПОБЕДИЛИ !";
        }
        result.text = "Ваш результат: " + Game.currentScore.ToString();
        gameOverMenu.SetActive(true);
    }
}

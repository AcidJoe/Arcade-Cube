using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SnakeManager : MonoBehaviour
{
    public Text score;
    public Text result;
    public GameObject gameOverMenu;

    public List<SnakeFood> food;

    public GameObject snakeFood;

    void Start ()
    {
        Game.currentScore = 0;
        gameOverMenu.SetActive(false);
        food = new List<SnakeFood>();
	}
	
	void Update ()
    {
        score.text = Game.currentScore.ToString();

	    if(food.Count < 1)
        {
            SpawnFood(foodSpawnPoint());
        }
	}

    void SpawnFood(Vector2 v)
    {
        var newFood = Instantiate(snakeFood, v, Quaternion.identity);
        food.Add(newFood as SnakeFood);
    }

    Vector2 ran()
    {
        int x = Random.Range(-15, 15);
        int y = Random.Range(-10, 10);

        return new Vector2(x, y);
    }

    bool Check(Vector2 vec)
    {
        if (Physics2D.OverlapCircle(vec, 0.2f))
        {
            return true;
        }

        return false;
    }

    Vector2 foodSpawnPoint()
    {
        Vector2 vec = ran();
        while (Check(vec))
        {
            vec = ran();
        }

        return vec;
    }

    public void BackToMenu()
    {
        if (Game.currentScore > Game.player.record_snake)
        {
            Game.player.CheckRank((int)Game.currentScore / 10);
            Game.player.record_snake = Game.currentScore;
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

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TetrisManager : MonoBehaviour
{
    public Text score;

    public Text result;

    public GameObject gameOverMenu;

	void Start ()
    {
        gameOverMenu.SetActive(false);
        Game.currentScore = 0;
	}
	
	void Update ()
    {
        score.text = Game.currentScore.ToString();
	}

    public void BackToMenu()
    {
        if(Game.currentScore > Game.player.record_tetris)
        {
            Game.player.CheckRank((int)Game.currentScore / 10);
            Game.player.record_tetris = Game.currentScore;
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

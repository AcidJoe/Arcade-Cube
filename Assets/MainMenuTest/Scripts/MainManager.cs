using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public Text greetings;
    public Text record;

    void Start ()
    {
        greetings.text = ("Привет " + Game.player.name);
        record.text = "Ваш рекорд в Тетрис: " + Game.player.record_tetris+"\nВаш рекорд в арканоид: "+Game.player.record_ark;
	}
	
	void Update ()
    {
	
	}

    public void PlayTetris()
    {
        SceneManager.LoadScene(2);
    }

    public void PlayArkanoid()
    {
        SceneManager.LoadScene(3);
    }
}

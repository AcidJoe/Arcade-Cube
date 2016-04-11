using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour {

    public int hitsToKill;
    public int points;
    private int numberOfHits;

    public GameObject oneHitDetector;

    // используйте этот метод для инициализации
    void Start()
    {
        points = 10;
        oneHitDetector.SetActive(false);
        numberOfHits = 0;
    }

    // Update вызывается при отрисовке каждого кадра игры
    void Update()
    {
        if(hitsToKill - numberOfHits == 1)
        {
            oneHitDetector.SetActive(true);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            numberOfHits++;

            if (numberOfHits == hitsToKill)
            {
                // уничтожаем объект
                StartCoroutine(_Destroy());
            }
        }
    }

    IEnumerator _Destroy()
    {
        Game.currentScore += points;
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}

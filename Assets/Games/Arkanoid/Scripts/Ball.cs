using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{
    private bool ballIsActive;
    private Vector3 ballPosition;
    private Vector2 ballInitialForce;

    public GameObject playerObject;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // создаем силу
        ballInitialForce = new Vector2(100.0f, 300.0f);

        // переводим в неактивное состояние
        ballIsActive = false;

        // запоминаем положение
        ballPosition = transform.position;
    }

    void Update()
    {
        if (!ballIsActive && playerObject != null)
        {
            // задаем новую позицию шарика
            ballPosition.x = playerObject.transform.position.x;
            ballPosition.y = playerObject.transform.position.y + 1.0f;

            // устанавливаем позицию шара
            transform.position = ballPosition;
        }
        // проверка нажатия на пробел
        if (Input.GetButtonDown("Jump") == true)
        {
            // проверка состояния
            if (!ballIsActive)
            {
                // сброс всех сил
                rb.isKinematic = false;
                // применим силу
                rb.AddForce(ballInitialForce);
                // зададим активное состояние
                ballIsActive = !ballIsActive;
            }
        }

        // проверка падения шара
        if (ballIsActive && transform.position.y < -4)
        {
            ballIsActive = !ballIsActive;
            ballPosition.x = playerObject.transform.position.x;
            ballPosition.y = playerObject.transform.position.y + 1.0f;
            transform.position = ballPosition;

            rb.isKinematic = true;
        }
    }
}
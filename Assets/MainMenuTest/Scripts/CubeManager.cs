using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CubeManager : MonoBehaviour
{
    public GameObject Cube;
    public GameObject diffCube;

    public ArcadeCube ac;
    public DiffCube dc;

    public GameObject cam;

    public Quaternion needAngle;

    public GameObject arcButton;
    public GameObject diffButton;
    public GameObject playerPanel;
    public GameObject gamePanel;

    public Text player_name, title, game, difficulty;

    Vector3 point;
    RaycastHit rh;
    Ray ray;

    bool isGameChosen = false;
    bool isDifficultyChosen = false;

	void Start ()
    {
        Game.player.CheckRank(0);
        gamePanel.SetActive(false);
	}
	

	void Update ()
    {
        player_name.text = Game.player.name;
        title.text = Game.player.curRatingName;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //if (Input.GetButtonDown("Fire1") && !ac)
        //{
        //    if (Physics.Raycast(ray))
        //        Instantiate(Cube, transform.position, transform.rotation);

        //    if (!ac)
        //    {
        //        ac = GameObject.FindGameObjectWithTag("ArcadeCube").GetComponent<ArcadeCube>();
        //    }
        //}

        if (ac && Input.GetButton("Fire1") && !ac.isStoped)
        {
            ac.rb.useGravity = false;
            ac.rb.AddTorque(200, 200, 200);
            if(Physics.Raycast(ray,out rh))
            {
                point = rh.collider.transform.position;
                point.y = 25.0f;
                ac.rb.velocity = (point - ac.transform.position).normalized * 2.0f;
            }
        }

        if (ac && Input.GetButtonUp("Fire1"))
        {
            ac.rb.useGravity = true;
            ac.rb.AddForce(ac.rb.velocity * 300.0f, ForceMode.Impulse);
        }

        if(ac && ac.isStoped)
        {
            game.text = ac.ChosenGame;
            ac.rb.mass = 10000.0f;
            isGameChosen = true;
        }

        //if (Input.GetButtonDown("Fire1") && !dc && isGameChosen)
        //{
        //    if (Physics.Raycast(ray))
        //        Instantiate(diffCube, transform.position, Quaternion.identity);

        //    if (!dc)
        //    {
        //        dc = GameObject.FindGameObjectWithTag("DiffCube").GetComponent<DiffCube>();
        //    }
        //}

        if (dc && Input.GetButton("Fire1") && !dc.isStoped)
        {
            dc.rb.useGravity = false;
            dc.rb.AddTorque(200, 200, 200);
            if (Physics.Raycast(ray, out rh))
            {
                point = rh.collider.transform.position;
                point.y = 25.0f;
                dc.rb.velocity = (point - dc.transform.position).normalized * 2.0f;
            }
        }

        if (dc && Input.GetButtonUp("Fire1"))
        {
            dc.rb.useGravity = true;
            dc.rb.AddForce(dc.rb.velocity * 300.0f, ForceMode.Impulse);
        }

        if (dc && dc.isStoped)
        {
            difficulty.text = dc.ChosenDiff;

            dc.rb.mass = 10000.0f;
            isDifficultyChosen = true;
        }

        if(isGameChosen && isDifficultyChosen)
        {
            if (cam.transform.position.y >= 5.0f)
            {
                cam.transform.position += (ac.transform.position - cam.transform.position).normalized * Time.deltaTime * 2.5f;
            }
            else
            {
                switch (ac.GameScene)
                {
                    case 2:
                        SceneManager.LoadScene(2);
                        break;
                    case 3:
                        SceneManager.LoadScene(3);
                        break;
                    case 4:
                        SceneManager.LoadScene(4);
                        break;
                    case 5:
                        SceneManager.LoadScene(5);
                        break;
                    case 6:
                        SceneManager.LoadScene(6);
                        break;
                    case 7:
                        SceneManager.LoadScene(7);
                        break;
                }
            }


            float rotAngle = ac.transform.eulerAngles.y;

            needAngle = Quaternion.Euler(90.0f, rotAngle, 0.0f);

            cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, needAngle, Time.deltaTime * 1.0f);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void ArcadeCubeSpawn()
    {
        if (playerPanel.activeInHierarchy)
        {
            playerPanel.SetActive(false);
        }

        if (!gamePanel.activeInHierarchy)
        {
            gamePanel.SetActive(true);
        }

        arcButton.SetActive(false);
        Instantiate(Cube, transform.position, transform.rotation);

        if (!ac)
        {
            ac = GameObject.FindGameObjectWithTag("ArcadeCube").GetComponent<ArcadeCube>();
        }
    }

    public void DiffCubeSpawn()
    {
        if (playerPanel.activeInHierarchy)
        {
            playerPanel.SetActive(false);
        }

        if (!gamePanel.activeInHierarchy)
        {
            gamePanel.SetActive(true);
        }

        diffButton.SetActive(false);
        Instantiate(diffCube, transform.position, Quaternion.identity);

        if (!dc)
        {
            dc = GameObject.FindGameObjectWithTag("DiffCube").GetComponent<DiffCube>();
        }
    }
}

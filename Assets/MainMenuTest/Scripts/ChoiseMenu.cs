using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChoiseMenu : MonoBehaviour
{

	void Start ()
    {
	
	}
	
	void Update ()
    {
	
	}

    public void Load(int i)
    {
        SceneManager.LoadScene(i);
    }
}

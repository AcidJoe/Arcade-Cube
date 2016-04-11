using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour
{
    public float moveSpeed;

    void Start ()
    {
	
	}
	
	void Update ()
    {
        transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);
    }
}

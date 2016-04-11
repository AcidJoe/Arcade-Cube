using UnityEngine;
using System.Collections;

public class Bang : MonoBehaviour
{
    void Awake()
    {
        StartCoroutine(die());
    }

    IEnumerator die()
    {
        yield return new WaitForSeconds(0.9f);
        Destroy(gameObject);
    }
}

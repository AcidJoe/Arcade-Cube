using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public int id;

    public Profile savedPlayer;

    public GameObject emptyPanel;
    public GameObject fullPanel;
    public GameObject newPanel;

    public InputField playerName;

    public Text profileName;

    void Start()
    {
        newPanel.SetActive(false);
    }
}

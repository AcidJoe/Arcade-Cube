using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Slot[] slots;

    void Start()
    {
        Check();
    }

    void Update()
    {

    }

    void Check()
    {
        for (int i = 1; i <= 3; i++)
        {
            if (SaveLoad.CheckFile(i))
            {
                foreach(Slot s in slots)
                {
                    if(s.id == i)
                    {
                        s.savedPlayer = SaveLoad.playerData(i);
                        s.profileName.text = s.savedPlayer.name;
                        s.newPanel.SetActive(false);
                        s.emptyPanel.SetActive(false);
                        s.fullPanel.SetActive(true);
                    }
                }
            }
            else
            {
                foreach (Slot s in slots)
                {
                    if (s.id == i)
                    {
                        s.newPanel.SetActive(false);
                        s.fullPanel.SetActive(false);
                        s.emptyPanel.SetActive(true);
                    }
                }
            }
        }
    }

    public void CreateNew(int slot)
    {
        foreach(Slot s in slots)
        {
            if(s.id == slot)
            {
                s.playerName.text = "";
                s.newPanel.SetActive(true);
                s.emptyPanel.SetActive(false);
                s.fullPanel.SetActive(false);
            }
        }
    }

    public void AddNewPlayer(int slot)
    {
        foreach (Slot s in slots)
        {
            if (s.id == slot)
            {
                s.savedPlayer = new Profile(s.playerName.text);
                Game.player = s.savedPlayer;
                SaveLoad.Save(slot);
                Check();
            }
        }
    }

    public void Play(int slot)
    {
        foreach (Slot s in slots)
        {
            if (s.id == slot)
            {
                Game.player = s.savedPlayer;
                Game.current_slot = s.id;

                SceneManager.LoadScene(1);
            }
        }
    }

    public void Delete(int slot)
    {
        foreach (Slot s in slots)
        {
            if (s.id == slot)
            {
                SaveLoad.DeleteFile(slot);
                Check();
            }
        }
    }
}

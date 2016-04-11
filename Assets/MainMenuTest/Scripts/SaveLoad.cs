using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoad
{
    public static void Save(int slot)
    {
        if (!Directory.Exists(Application.dataPath + "/Saves"))
        {
            Directory.CreateDirectory(Application.dataPath + "/Saves");
        }
        else if (Directory.Exists(Application.dataPath + "/Saves"))
        {
            FileStream fs = new FileStream(Application.dataPath + "/Saves/Slot"+slot+".aj", FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, Game.player);
            fs.Close();
        }
    }

    public static void Load(int slot)
    {
        if (File.Exists(Application.dataPath + "/Saves/Slot" + slot + ".aj"))
        {
            FileStream fs = new FileStream(Application.dataPath + "/Saves/Slot" + slot + ".aj", FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            Game.player = (Profile)bf.Deserialize(fs);
            fs.Close();
        }
    }

    public static Profile playerData(int slot)
    {
        if (File.Exists(Application.dataPath + "/Saves/Slot" + slot + ".aj"))
        {
            Profile p;
            FileStream fs = new FileStream(Application.dataPath + "/Saves/Slot" + slot + ".aj", FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            p = (Profile)bf.Deserialize(fs);
            fs.Close();

            return p;
        }
        else
        {
            return null;
        }
    }

    public static void DeleteFile(int slot)
    {
        if (File.Exists(Application.dataPath + "/Saves/Slot" + slot + ".aj"))
        {
            File.Delete(Application.dataPath + "/Saves/Slot" + slot + ".aj");
        }
    }

    public static bool CheckFile(int slot)
    {
        if (File.Exists(Application.dataPath + "/Saves/Slot" + slot + ".aj"))
        {
            return true;
        }
        else {
            return false;
        }
    }
}

public static class Game
{
    public static Profile player;

    public static int current_slot;

    public static int currentScore;

    public static int current_difficulty = 3;
}

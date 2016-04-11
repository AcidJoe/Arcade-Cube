using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuTest : MonoBehaviour
{
	void Start ()
    {
	
	}
	
	void Update ()
    {
	
	}
}

[System.Serializable]
public class Profile
{
    public string name;

    public int record_tetris;

    public int record_ark;

    public int record_ast;

    public int record_snake;

    public int record_pong;

    public int rating_points;

    public int nextRating;

    public int prevRating;

    public string curRatingName;
    public string NextRatingName;
    public string prevRatingName;


    public enum Rank
    {
        Noob,
        Newby,
        Player,
        Father,
        CyberGod
    }

    public Rank current;

    public Profile(string n)
    {
        name = n;

        record_tetris = 0;

        record_ark = 0;

        record_ast = 0;

        record_snake = 0;

        record_pong = 0;

        rating_points = 0;

        nextRating = 1000;

        prevRating = -1000;

        curRatingName = "Нуб";

        NextRatingName = "Криворукий";

        prevRatingName = "";

        SetRank(Rank.Noob);
    }

    public void SetRank(Rank rank)
    {
        switch (rank)
        {
            case Rank.Noob:
                nextRating = 100;
                prevRating = -10000000;

                curRatingName =  "Нуб";
                NextRatingName = "Криворукий";
                prevRatingName = "";
                break;
            case Rank.Newby:
                nextRating = 400;
                prevRating = 100;

                curRatingName = "Криворукий";
                NextRatingName = "Игрок";
                prevRatingName = "Нуб";
                break;
            case Rank.Player:
                nextRating = 800;
                prevRating = 400;

                curRatingName = "Игрок";
                NextRatingName = "Отец";
                prevRatingName = "Криворукий";
                break;
            case Rank.Father:
                nextRating = 1600;
                prevRating = 800;

                curRatingName = "Отец";
                NextRatingName = "Кибер-Бог";
                prevRatingName = "Игрок";
                break;
            case Rank.CyberGod:
                nextRating = 5000;
                prevRating = 1600;

                curRatingName = "Кибер-Бог";
                NextRatingName = "";
                prevRatingName = "Отец";
                break;
        }
        current = rank;
    }

    public void CheckRank(int score)
    {
        rating_points += score;
        
        if(rating_points >= nextRating)
        {
            switch (current)
            {
                case Rank.Noob:
                    SetRank(Rank.Newby);
                    break;
                case Rank.Newby:
                    SetRank(Rank.Player);
                    break;
                case Rank.Player:
                    SetRank(Rank.Father);
                    break;
                case Rank.Father:
                    SetRank(Rank.CyberGod);
                    break;
            }
        }

        if (rating_points <= prevRating)
        {
            switch (current)
            {
                case Rank.Newby:
                    SetRank(Rank.Noob);
                    break;
                case Rank.Player:
                    SetRank(Rank.Newby);
                    break;
                case Rank.Father:
                    SetRank(Rank.Player);
                    break;
                case Rank.CyberGod:
                    SetRank(Rank.Father);
                    break;
            }
        }
    }
}

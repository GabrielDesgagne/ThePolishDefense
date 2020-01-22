using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public int initialCurrency = 100;
    public float initialHp = 1;
    public int startingLevel = 0;
    public int damage = 5;

    public static int Headshot { get; set; }
    public static int PlayerKill { get; set; }
    public static int TowerKill { get; set; }
    public static int TotalKill { get { return PlayerKill + TowerKill; } }
    public static int Score { get; set; }
    public static bool IsPlayerDead { get; set; }

    private static int currency = 100;
    private static float hp;
    private static int currentLevel;

    //No need to implement init for now... resetAllStats() does it.

    public void resetAllStats()
    {
        IsPlayerDead = false;
        Currency = initialCurrency;
        Hp = initialHp;
        CurrentLevel = startingLevel;
    }

    public static int Currency
    {
        get { return currency; }
        private set { currency = value; }
    }

    public static float Hp
    {
        get { return hp; }
        set { hp = value; }
    }

    public static int CurrentLevel
    {
        get { return currentLevel; }
        private set { currentLevel = value; }
    }

    public static void addCurrency(int amount)
    {
        Currency += amount;
    }

    public static bool subtractCurrency(int amount)
    {
        if (Currency - amount < 0)
        {
            return false;
        }
        else
        {
            Currency -= amount;
            return true;
        }
    }

    public static void addHp(float amount)
    {
        Hp += amount;
    }

    public static void decrementHp()
    {
        if (--Hp == 0)
        {
            IsPlayerDead = true;
        }
    }

    public void subtractHp(int amount)
    {
        if (hp - amount <= 0)
        {
            hp = 0;
            IsPlayerDead = true;
        }
        else
        {
            hp -= amount;
        }
    }

    public static void nextLevel()
    {
        CurrentLevel++;
    }

}

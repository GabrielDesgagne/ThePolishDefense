using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public int initialCurrency = 100;
    public int initialHp = 10;
    public int startingLevel = 0;
    public int damage = 5;
    
    public static bool IsPlayerDead { get; set; }

    private static int currency=100;
    private static int hp;
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

    public static int Hp
    {
        get { return hp; }
        private set { hp = value; }
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
        if(Currency - amount < 0)
        {
            return false;
        }
        else
        {
            Currency -= amount;
            return true;
        }
    }

    public static void addHp(int amount)
    {
        Hp += amount;
    }

    public static void decrementHp()
    {
        if(--Hp == 0)
        {
            IsPlayerDead = true;
        }
    }

    public void subtractHp(int amount)
    {
        if(hp - amount <= 0)
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public int initialCurrency = 100;
    public int initialHp = 10;
    public int startingLevel = 1;

    public bool IsPlayerDead { get; set; }

    private int currency;
    private int hp;
    private int currentLevel;

    //No need to implement init for now... resetAllStats() does it.

    public void resetAllStats()
    {
        IsPlayerDead = false;
        Currency = initialCurrency;
        Hp = initialHp;
        CurrentLevel = startingLevel;
    }

    public int Currency
    {
        get { return currency; }
        private set { currency = value; }
    }

    public int Hp
    {
        get { return hp; }
        private set { hp = value; }
    }

    public int CurrentLevel
    {
        get { return currentLevel; }
        private set { currentLevel = value; }
    }

    public void addCurrency(int amount)
    {
        Currency += amount;
    }

    public bool subtractCurrency(int amount)
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

    public void addHp(int amount)
    {
        Hp += amount;
    }

    public void decrementHp()
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

    public void nextLevel()
    {
        CurrentLevel++;
    }

}

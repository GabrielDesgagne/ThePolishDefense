using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestCommand : MonoBehaviour
{
    public RoundOver round;
    public TMP_InputField input;
    public GameObject canvas;
    public GameObject amountIN;
    [SerializeField] TextUI textUI;
    [SerializeField] TextManager textM;
    [SerializeField] SFXManager sfx;
    public int number;
    int choice;
    // Start is called before the first frame update



    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        Refresh();
    }
    public void Initialize()
    {

    }
    public void Refresh()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            canvas.SetActive(true);
            amountIN.SetActive(false);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            canvas.SetActive(false);
        }

    }
    public void CheckAmount()
    {
        int num = int.Parse(amountIN.GetComponent<TMP_InputField>().text);
        switch (choice)
        {
            case 1:
                textUI.AddScore(num);
                break;
            case 2:
                textUI.AddCash(num);
                break;
            case 3:
                textUI.AddHeadshot(num);
                break;
            case 4:
                textUI.AddPlayerKill(num);
                break;
            case 5:
                textUI.AddTowerKill(num);
                break;
        }
    }

    public void CheckCheat()
    {

        if (input.text == "win")
        {
            round.ShowVictory();
        }
        else if (input.text == "lose")
        {
            round.ShowDefeat();
        }
        else if (input.text == "hide")
        {
            round.HideUI();
        }
        else if (input.text == "reset")
        {
            textUI.Reset();
        }
        else if (input.text == "turn on")
        {
            textM.TurningOn();
        }
        else if (input.text == "turn off")
        {
            textM.TurningOff();
        }
        else if (input.text == "add score")
        {
            amountIN.SetActive(true);
            choice = 1;
        }
        else if (input.text == "add cash")
        {
            amountIN.SetActive(true);
            choice = 2;
        }
        else if (input.text == "add headshot")
        {
            amountIN.SetActive(true);
            choice = 3;
        }
        else if (input.text == "add player kill")
        {
            amountIN.SetActive(true);
            choice = 4;
        }
        else if (input.text == "add tower kill")
        {
            amountIN.SetActive(true);
            choice = 5;
        }
        else if (input.text == "no amount")
        {
            amountIN.SetActive(false);
        }
        else if (input.text == "attack by assassin")
        {
            sfx.Soundtrack(input.text);
        }
        else if (input.text == "skyrim")
        {
            sfx.Soundtrack(input.text);
        }
        else if (input.text == "grab1")
        {
            sfx.PlayerSound(input.text);
        }
        else if (input.text == "grab2")
        {
            sfx.PlayerSound(input.text);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestCommand : MonoBehaviour
{
    public RoundOver round;
    public TMP_InputField input;
    public GameObject canvas;
    public TextUI textUI;
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        Refresh();
    }

    void Initialize()
    {
       
    }
    void Refresh()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            canvas.SetActive(true);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            canvas.SetActive(false);
        }

        if (input.text == "win")
        {
            round.showVictory();
        }
        if (input.text == "lose")
        {
            round.showDefeat();
        }
        if (input.text == "hide")
        {
            round.hideUI();
        }
        if (input.text == "all clear")
        {
            textUI.clearStats();
        }
        if (input.text == "kill clear")
        {
            textUI.clearKills();
        }

        /*
        if (Input.GetKey(KeyCode.Z))
        {
            round.addScore();
        }
        if (Input.GetKey(KeyCode.X))
        {
            round.subtractScore();
        }
        if (Input.GetKey(KeyCode.C))
        {
            round.addCash();
        }
        if (Input.GetKey(KeyCode.V))
        {
            round.subtractCash();
        }
        if (Input.GetKey(KeyCode.B))
        {
            round.addHeadshot();
        }
        if (Input.GetKey(KeyCode.N))
        {
            round.subtractHeadshot();
        }
        if (Input.GetKey(KeyCode.F))
        {
            round.addPlayerKill();
        }
        if (Input.GetKey(KeyCode.G))
        {
            round.subtractPlayerKill();
        }
        if (Input.GetKey(KeyCode.H))
        {
            round.addTowerKill();
        }
        if (Input.GetKey(KeyCode.J))
        {
            round.subtractTowerKill();
        }
        if (Input.GetKey(KeyCode.M))
        {
            round.clearStats();
        }
        if (Input.GetKey(KeyCode.L))
        {
            round.clearKills();
        }
        */
    }
}

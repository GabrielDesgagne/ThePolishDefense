using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCommand : MonoBehaviour
{
     public RoundOver round;
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
        if (Input.GetKey(KeyCode.Q))
        {
            round.showVictory();
        }
        if (Input.GetKey(KeyCode.E))
        {
            round.hideUI();
        }
        if (Input.GetKey(KeyCode.R))
        {
            round.showDefeat();
        }
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
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{

    static public Main instance { get; private set; }

    private Game game;
    private Room room;
    private Flow currentFlow;

    private bool isInRoomScene = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        //Instead of game.PreInitialize....
        //currenFlow.PreInitialize

        game = Game.Instance;
        room = Room.Instance;

        //Starting Instance
        currentFlow = room;
        currentFlow.PreInitialize();
    }

    private void Start()
    {
        currentFlow.Initialize();
    }

    private void Update()
    {
        currentFlow.Refresh();
    }

    private void FixedUpdate()
    {
        currentFlow.PhysicsRefresh();
    }

    public void ChangeCurrentFlow()
    {
        if (isInRoomScene)
        {
            currentFlow = game;
            currentFlow.PreInitialize();
            currentFlow.Initialize();
        }
        else if (!isInRoomScene)
        {
            currentFlow = room;
            currentFlow.PreInitialize();
            currentFlow.Initialize();
        }
    }
}

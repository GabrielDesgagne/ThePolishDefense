using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{

    static public Main instance { get; private set; }

    private Game game;
    private Room room;
    private Flow currentFlow;

    public bool startsInRoomScene = true;

    private void Awake()
    {
        //Singleton
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        //Initialize
        game = Game.Instance;
        room = Room.Instance;

        //Load Flow
        ChangeCurrentFlow();
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
        if (startsInRoomScene)
        {
            currentFlow = game;
            currentFlow.PreInitialize();
            currentFlow.Initialize();
        }
        else if (!startsInRoomScene)
        {
            currentFlow = room;
            currentFlow.PreInitialize();
            currentFlow.Initialize();
        }
    }
}

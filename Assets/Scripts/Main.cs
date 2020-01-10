using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{

    static public Main instance { get; private set; }

    private Game game;

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

        game = Game.Instance;
        game.PreInitialize();

    }

    private void Start()
    {
        game.Initialize();
    }

    private void Update()
    {
        game.Refresh();
    }

    private void FixedUpdate()
    {
        game.PhysicsRefresh();
    }

}

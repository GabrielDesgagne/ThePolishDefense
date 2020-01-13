using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{

    static public Main Instance { get; private set; }

    private Game game;
    private Room room;
    private Flow currentFlow;

    public Global GlobalVariables;
    public GameObject RoomSetupPrefab;
    public GameObject GameSetupPrefab;
    public GameObject VRPlayerCharacter;

    public SceneTransition sceneTransition;

    private bool isInRoomScene = true;

    private void Awake()
    {

        #region Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        #endregion

        //Initialize
        game = Game.Instance;
        room = Room.Instance;

        //Loads
        //RoomSetupPrefab = Resources.Load<GameObject>("Prefabs/Room/RoomSetup");
        //GameSetupPrefab = Resources.Load<GameObject>("Prefabs/Game/GameSetup");

        //Get/Set
        GlobalVariables = gameObject.GetComponent<Global>();
        sceneTransition = gameObject.GetComponent<SceneTransition>();

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

    private void EndFlow()
    {
        currentFlow.EndFlow();
    }

    public void ChangeCurrentFlow()
    {
        EndFlow();

        if (!isInRoomScene)
        {
            sceneTransition.loadMainRoomScene();
            currentFlow = room;
            isInRoomScene = true;
        }
        else 
        {
            sceneTransition.loadMainMapScene();
            currentFlow = game;
            isInRoomScene = false;
        }

        currentFlow.PreInitialize();
        currentFlow.Initialize();
    }
}

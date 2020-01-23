using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{

    static public Main Instance { get; private set; }

    private Game game;
    private Room room;
    private Flow currentFlow;


    [Header("Internal Settings")]
    public Global GlobalVariables;
    public GameObject RoomSetupPrefab;
    [HideInInspector]
    public GameObject VRPlayerCharacter;

    public Dictionary<GameObject, GrabbableObject> grabbableObjects;
    public Dictionary<GameObject, InteractObject> interactObjects;

    //public Player playerPrefab;
    public SceneTransition sceneTransition;

    public bool isInRoomScene { get; private set; }

    private string currentSceneName;
    private string lastSceneName;

    public AmbianceManager ambiance;

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
            return;
        }
        #endregion

        //Vars
        isInRoomScene = true;

        //Initialize
        game = Game.Instance;
        room = Room.Instance;
        ambiance = AmbianceManager.Instance;
        grabbableObjects = new Dictionary<GameObject, GrabbableObject>();
        interactObjects = new Dictionary<GameObject, InteractObject>();
        //Get/Set
        sceneTransition = gameObject.GetComponent<SceneTransition>();
        ambiance.Initialize();
        //Scene Loading Delegate
        SceneManager.sceneLoaded += OnSceneLoaded;
        if (SceneManager.GetActiveScene().name == "RoomScene")
        {
            currentFlow = room;
        }
        else if (SceneManager.GetActiveScene().name == "MapScene")
        {
            currentFlow = game;
            ambiance.playMapMusic();
            isInRoomScene = false;
        }
        else
        {
            Debug.LogWarning("Not supposed to happen. Wrong scene name. Loading Room Flow.");
            currentFlow = room;
        }
    }

    private void Start()
    {
        //currentFlow.Initialize();
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
            isInRoomScene = true;
        }
        else
        {
            sceneTransition.loadMainMapScene();
            isInRoomScene = false;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentSceneName = scene.name;

        if (!isInRoomScene)
        {
            currentFlow = game;
        }
        else
        {
            currentFlow = room;
        }
        //Make sure this is called only once.
        if (currentSceneName != lastSceneName)
        {
            currentFlow.PreInitialize();
            currentFlow.Initialize();
            lastSceneName = currentSceneName;
        }
    }
}

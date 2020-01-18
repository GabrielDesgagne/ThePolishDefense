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
    public GameObject GameSetupPrefab;
    public Player playerPrefab;
    public SceneTransition sceneTransition;

    public Dictionary<GameObject, GrabbableObject> grabbableObjects;
    public bool isInRoomScene { get; private set; }
    
    private string currentSceneName;
    private string lastSceneName;
    
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

        //Vars
        isInRoomScene = true;

        //Initialize
        game = Game.Instance;
        room = Room.Instance;
        grabbableObjects = new Dictionary<GameObject, GrabbableObject>();

        //Get/Set
        GlobalVariables = gameObject.GetComponent<Global>();
        sceneTransition = gameObject.GetComponent<SceneTransition>();

        //Scene Loading Delegate
        SceneManager.sceneLoaded += OnSceneLoaded;
        if (SceneManager.GetActiveScene().name == "RoomScene")
        {
            currentFlow = room;
        }
        else if (SceneManager.GetActiveScene().name == "MapScene")
        {
            currentFlow = game;
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
        Debug.Log("OnSceneLoaded: " + scene.name);
        //Debug.Log(mode);

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
        if(currentSceneName != lastSceneName)
        {
            currentFlow.PreInitialize();
            currentFlow.Initialize();
            lastSceneName = currentSceneName;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopController : MonoBehaviour
{
    private bool restart;
    private GameObject towerPrefab;
    private GameObject trapPrefab;
    public GameObject[] shopInventory = new GameObject[8];
    private Transform spawnPos;
    private int spawnType=0;
    public GameObject TowerBoard;
    public GameObject TrapBoard;
    private string spawnName;
    private bool spawn = false;
    private float spawnTimer = 3f;
    private bool timeStart = false;
    private Dictionary<GameObject, Transform> spawningDict = new Dictionary<GameObject, Transform>();
    private Dictionary<GameObject, int> prefabTypeDict = new Dictionary<GameObject, int>();
    [SerializeField] private Transform[] spawnPoints = new Transform[8];
    // Start is called before the first frame update
    void Start()
    {
        PreInit();
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (OVRInput.Get(OVRInput.Button.One) || Input.GetKeyDown(KeyCode.R))
        {
            restart = true;
        }
        if (restart)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }


        if (spawn)
        {
            spawn = false;
            StartCoroutine(spawnItem(spawnType, spawnPos, spawnName));
        } 
      
    }



    void PreInit()
    {

        towerPrefab = Resources.Load<GameObject>("Prefabs/Tower");
        trapPrefab = Resources.Load<GameObject>("Prefabs/Trap");


        shopInventory[0] = Instantiate(towerPrefab, spawnPoints[0].position, TowerBoard.transform.rotation);
        shopInventory[0].name = "Tower1";
        spawningDict.Add(shopInventory[0], spawnPoints[0]);
        prefabTypeDict.Add(shopInventory[0], 1);

        shopInventory[1] = Instantiate(towerPrefab, spawnPoints[1].position, TowerBoard.transform.rotation);
        shopInventory[1].name = "Tower2";
        spawningDict.Add(shopInventory[1], spawnPoints[1]);
        prefabTypeDict.Add(shopInventory[1], 1);

        shopInventory[2] = Instantiate(towerPrefab, spawnPoints[2].position, TowerBoard.transform.rotation);
        shopInventory[2].name = "Tower3";
        spawningDict.Add(shopInventory[2], spawnPoints[2]);
        prefabTypeDict.Add(shopInventory[2], 1);

        shopInventory[3] = Instantiate(towerPrefab, spawnPoints[3].position, TowerBoard.transform.rotation);
        shopInventory[3].name = "Tower4";
        spawningDict.Add(shopInventory[3], spawnPoints[3]);
        prefabTypeDict.Add(shopInventory[3], 1);

        shopInventory[4] = Instantiate(trapPrefab, spawnPoints[4].position, TrapBoard.transform.rotation);
        shopInventory[4].name = "Trap1";
        spawningDict.Add(shopInventory[4], spawnPoints[4]);
        prefabTypeDict.Add(shopInventory[4], 2);

        shopInventory[5] = Instantiate(trapPrefab, spawnPoints[5].position, TrapBoard.transform.rotation);
        shopInventory[5].name = "Trap2";
        spawningDict.Add(shopInventory[5], spawnPoints[5]);
        prefabTypeDict.Add(shopInventory[5], 2);

        shopInventory[6] = Instantiate(trapPrefab, spawnPoints[6].position, TrapBoard.transform.rotation);
        shopInventory[6].name = "Trap3";
        spawningDict.Add(shopInventory[6], spawnPoints[6]);
        prefabTypeDict.Add(shopInventory[6], 2);

        shopInventory[7] = Instantiate(trapPrefab, spawnPoints[7].position, TrapBoard.transform.rotation);
        shopInventory[7].name = "Trap4";
        spawningDict.Add(shopInventory[7], spawnPoints[7]);
        prefabTypeDict.Add(shopInventory[7], 2);
    }


    void Init()
    {

    }

    void Refresh()
    {

    }

    void PhysicsRefresh()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("TrigEnter");
    }

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("TrigStay");
    }

    private void OnTriggerExit(Collider other)
    {
        //Match the exiting collider with the right prefab and spawn location 

        //Debug.Log("TrigExit");

        foreach(KeyValuePair<GameObject,Transform> obj in spawningDict)
        {
            if(other.name == obj.Key.name)
            {
                foreach(KeyValuePair<GameObject,int> prefab in prefabTypeDict)
                {
                    if(prefab.Key.name == other.name)
                    {
                        spawnType = prefab.Value;
                        spawnName = other.name;
                    }
                }
                spawnPos = obj.Value;
                spawn = true;
            }
        }
    }

    private IEnumerator spawnItem(int type, Transform pos, string name)
    {
        yield return new WaitForSeconds(3);
        int index=-1;
        for(int i = 0; i < 8; i++)
        {
            if(shopInventory[i].name == name)
            {
                index = i;
            }
        }
        if (type == 1 && index>=0)
        {
            shopInventory[index] = Instantiate(towerPrefab, pos.position, TowerBoard.transform.rotation);
            shopInventory[index].name = name;
            spawningDict.Add(shopInventory[index], spawnPoints[index]);
            prefabTypeDict.Add(shopInventory[index], type);
        }
        else if(type == 2 && index >= 0)
        {
            shopInventory[index] = Instantiate(trapPrefab, pos.position, TrapBoard.transform.rotation);
            shopInventory[index].name = name;
            spawningDict.Add(shopInventory[index], spawnPoints[index]);
            prefabTypeDict.Add(shopInventory[index], type);
        }

    }
}

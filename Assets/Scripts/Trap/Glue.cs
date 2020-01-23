using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glue : MonoBehaviour {
    //Door gameObject
    [SerializeField] private GameObject door1;
    [SerializeField] private GameObject door2;

    //Door position animation
    //Vector3 startPosDoor1 = new Vector3(, 25, 25);
    Vector3 endPosDoor1 = new Vector3(-2.25f, 0, -2f);
    Vector3 startPosDoor2 = new Vector3(-2.25f, 0, 0);
    Vector3 endPosDoor2 = new Vector3(-2.25f, 0, 1);

    public void OnTriggerEnter(Collider other)
    {
        door1.transform.localPosition = new Vector3(-2.3f, 0.1f, -2);
        door2.transform.localPosition = new Vector3(-2.3f, 0.1f, 1);
        EnemyManager.Instance.SlowEnemiesInRange(transform.position, 5f, 0.50f, 2f);
    }

    public void OnTriggerExit(Collider other)
    {
        door1.transform.localPosition = new Vector3(-2.3f, 0.1f, -1.2f);
        door2.transform.localPosition = new Vector3(-2.3f, 0.1f, 0);
    }
}

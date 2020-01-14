using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombFeeder {
    public GameObject Object { get; private set; }
    public Vector3 Position { get; set; }
    public bool IsReady { get { return BombList[0].transform.position.y >= Position.y + topY; } }
    public List<GameObject> BombList = new List<GameObject>();//temporary list

    public float topY { get; private set; }

    public BombFeeder() {
        Position = new Vector3(0, 0, 0);
    }

    public BombFeeder(Vector3 position) {
        Position = position;
        Object = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/BombFeeder"), Position, Quaternion.identity);
    }

    GameObject bombPrefab;
    public void SpawnBombs() {
        bombPrefab = ProjectileManager.Instance.projectilePrefab[ProjectileType.BOMB];
        bombPrefab = Resources.Load<GameObject>("Prefabs/Bomb");
        float offSet = bombPrefab.transform.localScale.y * 0.25f;
        topY = bombPrefab.transform.localScale.y * 2 + offSet;

        BombList.Add(GameObject.Instantiate(bombPrefab, Position + new Vector3(0, topY, 0), Quaternion.identity));
        BombList.Add(GameObject.Instantiate(bombPrefab, Position + new Vector3(0, topY - bombPrefab.transform.localScale.y, 0), Quaternion.identity));
        BombList.Add(GameObject.Instantiate(bombPrefab, Position + new Vector3(0, offSet, 0), Quaternion.identity));
    }

    public void Move() {
        float speed = 0.05f;
        if (BombList[0].transform.position.y < Position.y + topY) {
            BombList[0].transform.position += new Vector3(0, speed, 0);
        }
        if (BombList[1].transform.position.y < Position.y + topY - bombPrefab.transform.localScale.y) {
            BombList[1].transform.position += new Vector3(0, speed, 0);
        }
        if (BombList[2].transform.position.y < Position.y + topY - (bombPrefab.transform.localScale.y * 2)) {
            BombList[2].transform.position += new Vector3(0, speed, 0);
        }
    }

    public void Pickup() {
        if (BombList[0].transform.position.y >= Position.y + topY) {
            GameObject.Destroy(BombList[0]);//tmeporary until we have a object pool in the ProjectileManager & wont destroy until collision
            BombList.RemoveAt(0);
            BombList.Add(GameObject.Instantiate(bombPrefab, BombList[1].transform.position - new Vector3(0, bombPrefab.transform.localScale.y, 0), Quaternion.identity));//temporary get this from the pool
        }
    }
}

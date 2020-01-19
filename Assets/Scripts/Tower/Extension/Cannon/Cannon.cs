using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon
{
    public GameObject Obj { get; set; }
    public Tower Tower { get; set; }
    public bool IsLoaded { get; set; }
    public float Angle { get; set; }
    public Bomb Bomb { get; set; }
    private float DistanceFromCenter { get; set; }

    public Cannon(Tower tower, Vector3 position)
    {
        Obj = GameObject.Instantiate(TowerManager.Instance.cannonPrefab, position, Quaternion.identity, tower.Obj.transform);
        DistanceFromCenter = Obj.transform.position.x - tower.Obj.transform.position.x;
        Tower = tower;
        Angle = 0;
    }

    public void CannonInput()
    {
        if (Input.GetKey(KeyCode.A))
            Angle += 1f;
        if (Input.GetKey(KeyCode.D))
            Angle -= 1f;
        if (Input.GetKeyDown(KeyCode.S))
            Fire(TowerManager.Instance.GetTarget());
        if (Angle > 180)
            Angle = -180;
        if (Angle < -180)
            Angle = 180;
    }

    public float GetAngleToTarget(Vector3 target)
    {
        float angle = 0;
        Vector3 towerCenter = Tower.Obj.transform.position;

        float distA = target.x - towerCenter.x;
        float distB = target.z - towerCenter.z;

        angle = Mathf.Atan2(distB, distA) * Mathf.Rad2Deg;
        return angle;
    }

    public void Move(Vector3 towerCenter)
    {
        float radian = Angle * Mathf.PI / 180;
        Vector3 vec = Obj.transform.position;

        Obj.transform.position = new Vector3(towerCenter.x + (DistanceFromCenter * Mathf.Cos(radian)), vec.y, towerCenter.z + (DistanceFromCenter * Mathf.Sin(radian)));
        Obj.transform.rotation = Quaternion.Euler(0, -Angle, 0);
    }

    public void AngleMoveToTarget(Vector3 target)
    {
        float distance = (GetAngleToTarget(target) - Angle) % 360;
        if (distance > 0)
            Angle += 1;
        else
            Angle -= 1;
    }

    public void Fire(Vector3 target)
    {
        if (IsLoaded)
        {
            Bomb.StartPos = Obj.transform.position;
            Bomb.TargetPos = target;
            Bomb.Obj.SetActive(true);
            IsLoaded = false;
        }
    }

    public void LoadCannon(Bomb bomb)
    {
        Bomb = bomb;
        IsLoaded = true;
    }
}

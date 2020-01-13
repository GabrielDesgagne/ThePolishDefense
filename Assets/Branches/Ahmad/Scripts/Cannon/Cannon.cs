using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour {

    public GameObject Object { get; set; }
    public bool IsLoaded { get; set; }
    public float Angle { get; set; }
    public Bomb Bomb { get; set; }

    public Cannon(Vector3 position) {
        Object = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Cannon"), position, Quaternion.identity);
        Angle = 0;
    }

    public void CannonInput() {
        if (Input.GetKey(KeyCode.A))
            Angle += 1f;
        if (Input.GetKey(KeyCode.D))
            Angle -= 1f;
        if (Input.GetKeyDown(KeyCode.S))
            Fire(new Vector3(100, 10, 0));
        if (Angle > 360)
            Angle = 0;
        if (Angle < 0)
            Angle = 360;
    }

    public void Move(Vector3 towerCenter) {
        float radian = Angle * Mathf.PI / 180;
        Vector3 vec = Object.transform.position;
        Object.transform.position = new Vector3(towerCenter.x + (14.25f * Mathf.Cos(radian)), vec.y, towerCenter.z + (14.25f * Mathf.Sin(radian)));
        Object.transform.rotation = Quaternion.Euler(0, -Angle, 0);
    }

    public void Fire(Vector3 target) {
        if (IsLoaded) {
            Bomb.StartPos = Object.transform.position;
            Bomb.TargetPos = target;
            Bomb.Object.SetActive(true);
            IsLoaded = false;
        }
    }

    public void LoadCannon(Bomb bomb) {
        Bomb = bomb;
        IsLoaded = true;
    }
}

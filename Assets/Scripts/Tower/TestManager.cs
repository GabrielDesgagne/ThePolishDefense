using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    void Awake()
    {
        ProjectileManager.Instance.PreInitialize();
        TowerManager.Instance.PreInitialize();
    }

    void Start()
    {
        ProjectileManager.Instance.Initialize();
        TowerManager.Instance.Initialize();
    }

    void Update()
    {
        ProjectileManager.Instance.Refresh();
        TowerManager.Instance.Refresh();
    }

    void FixedUpdate()
    {
        ProjectileManager.Instance.PhysicsRefresh();
        TowerManager.Instance.PhysicsRefresh();
    }
}

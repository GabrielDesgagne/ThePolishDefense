using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    void Awake()
    {
        TowerLink.tl = GameObject.FindObjectOfType<TowerLink>();
        ProjectileManager.Instance.PreInitialize();
        TowerManager.Instance.PreInitialize();
        TimeManager.Instance.PreInitialize();
    }

    void Start()
    {
        ProjectileManager.Instance.Initialize();
        TowerManager.Instance.Initialize();
        TimeManager.Instance.Initialize();
    }

    void Update()
    {
        ProjectileManager.Instance.Refresh();
        TowerManager.Instance.Refresh();
        TimeManager.Instance.Refresh();
    }

    void FixedUpdate()
    {
        ProjectileManager.Instance.PhysicsRefresh();
        TowerManager.Instance.PhysicsRefresh();
        TimeManager.Instance.PhysicsRefresh();
    }
}

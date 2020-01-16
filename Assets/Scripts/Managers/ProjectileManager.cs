using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : Flow {
    #region Singleton
    static private ProjectileManager instance = null;

    static public ProjectileManager Instance
    {
        get
        {
            return instance ?? (instance = new ProjectileManager());
        }
    }

    #endregion

    public Dictionary<ProjectileType, List<Projectile>> enabledProjectiles;
    public Dictionary<ProjectileType, List<Projectile>> disabledProjectiles;
    public Dictionary<ProjectileType, GameObject> projectilePrefab;

    public ParticleSystem explosionParticle;
    public ParticleSystem sparkParticle;

    override public void PreInitialize()
    {
        enabledProjectiles = new Dictionary<ProjectileType, List<Projectile>>();
        disabledProjectiles = new Dictionary<ProjectileType, List<Projectile>>();
        InitDictionaries();

        projectilePrefab = new Dictionary<ProjectileType, GameObject>();

        projectilePrefab.Add(ProjectileType.BOMB, Resources.Load<GameObject>("Prefabs/Tower/Bomb"));
        projectilePrefab.Add(ProjectileType.POTION, Resources.Load<GameObject>("Prefabs/Tower/Ice_Potion"));

        explosionParticle = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Tower/Particles/ExplosionEffect")).GetComponent<ParticleSystem>();
        explosionParticle.Stop();
        sparkParticle = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Tower/Particles/SparkEffect")).GetComponent<ParticleSystem>();
        sparkParticle.Stop();
    }

    override public void Initialize()
    {
        GameObject bombParent = new GameObject("BombParent");
        GameObject potionParent = new GameObject("PotionParent");
        for (int i = 0; i < 10; i++)
        {
            Bomb bomb = new Bomb(GameObject.Instantiate(projectilePrefab[ProjectileType.BOMB], bombParent.transform));
            bomb.Obj.SetActive(false);
            disabledProjectiles[bomb.Type].Add(bomb);

            Potion potion = new Potion(GameObject.Instantiate(projectilePrefab[ProjectileType.POTION], potionParent.transform));
            potion.Obj.SetActive(false);
            disabledProjectiles[potion.Type].Add(potion);
        }
    }

    override public void Refresh()
    {
        MoveProjectiles();
    }

    override public void PhysicsRefresh()
    {

    }

    override public void EndFlow()
    {

    }

    public void BasicShoot(ProjectileType type, Vector3 startPos, Vector3 targetPos)
    {
        Projectile projectile = disabledProjectiles[type][0];
        projectile.StartPos = startPos;
        projectile.TargetPos = targetPos;
        projectile.Obj.SetActive(true);
        enabledProjectiles[projectile.Type].Add(projectile);
        disabledProjectiles[type].RemoveAt(0);
    }

    public void MoveProjectiles()
    {
        foreach (ProjectileType type in System.Enum.GetValues(typeof(ProjectileType)))
        {
            for (int i = 0; i < enabledProjectiles[type].Count; i++)
            {
                if (enabledProjectiles[type][i].SlerpPct < 1)
                {
                    if (enabledProjectiles[type][i].Obj.activeSelf)
                    {
                        enabledProjectiles[type][i].MoveToTarget();
                    }
                }
                else
                {
                    enabledProjectiles[type][i].CollisionHit();
                    ResetProjectile(enabledProjectiles[type][i]);
                }
            }
        }
    }

    public void ResetProjectile(Projectile projectile)
    {
        projectile.Reset();
        disabledProjectiles[projectile.Type].Add(projectile);
        enabledProjectiles[projectile.Type].Remove(projectile);
    }

    public bool IsReadyToShoot(ProjectileType type)
    {
        return this.disabledProjectiles[type].Count > 0;
    }

    private void InitDictionaries()
    {
        foreach (ProjectileType type in System.Enum.GetValues(typeof(ProjectileType)))
        {
            enabledProjectiles.Add(type, new List<Projectile>());
            disabledProjectiles.Add(type, new List<Projectile>());
        }
    }
}

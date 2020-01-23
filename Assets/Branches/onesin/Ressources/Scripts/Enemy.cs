using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class Enemy : MonoBehaviour {

    private EnemyManager enemyManager;
    private TimeManager timeManager;

    public float startSpeed = 10;
    [HideInInspector]
    public float speed;
    public float startHealth = 100f;
    private float health;
    public int value = 50;

    //public EnemyType type;

    public Image healthBar;
    [HideInInspector]
    public bool isDead = false;


    private Animator anim;
    public GameObject audioEnnemi;
    public AudioClip soundDead;
    private EnemyMovement mvt;
    private AudioSource dead;
    public GameObject uiDiedEffectPrefabs;
    private float speedMoveUi = 2f;
    public GameObject bloodEffect;
    [HideInInspector]
    public bool isHittable;
    [HideInInspector]
    public bool canEnter;//to check if enemy is able to enter the player base
    private float openDoorTime = 0;
    public float stateDuration = 1.8f;
    public float MaxStateDuration = 1.8f;

    private Transform target;
    private int waypointIndex = 0;

    private Rigidbody rb;

    public void Initialize()
    {
        enemyManager = EnemyManager.Instance;
        timeManager = TimeManager.Instance;

        speed = startSpeed;
        health = startHealth;
        if (enemyManager.waypoints.Length > 0)
            target = enemyManager.waypoints[0];
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        mvt = GetComponent<EnemyMovement>();
        //walk = audioEnnemi.GetComponent<AudioSource>();
        //dead = audioEnnemi.GetComponent<AudioSource>();
        isHittable = true;
        canEnter = false;
    }

    public void Refresh()
    {

        if (canEnter)
        {
            openDoorTime += Time.deltaTime;
            if (isHittable)
            {
                anim.SetTrigger("idle");
                //anim.SetBool("isWalk", false);
            }
            //speed = 0;
            isHittable = false;
        }
        if (openDoorTime > stateDuration)
        {
            openDoorTime = 0;
            canEnter = false;
            //speed = startSpeed;
            anim.SetBool("isWalk", true);
            //anim.SetBool("idle", false);

        }
        //for test the take damage function. we can delete it after
        if (Input.GetKeyDown(KeyCode.A)/*&&isHittable*/)
        {
            TakeDamage(50);
        }
        //

        if (enemyManager.waypoints.Length > 0)
        {
            if (Vector3.Distance(transform.position, target.position) <= 0.3f)
            {
                //check the next way point
                if (waypointIndex >= enemyManager.waypoints.Length - 1)
                {
                    PlayerStats.decrementHp();
                    isHittable = false;
                    WaveSpawner.EnemiesAlive--;
                    enemyManager.EnemyDied(this);
                    return;
                }

                waypointIndex++;
                target = enemyManager.waypoints[waypointIndex];
            }
        }
    }

    public void PhysicsRefresh()
    {
        if (enemyManager.waypoints.Length > 0)
        {
            Vector3 dir = target.position - transform.position;
            transform.LookAt(target);
            if (!canEnter)
            {
                rb.MovePosition(dir.normalized * speed * Time.fixedDeltaTime + rb.position);
            }
        }
    }

    public void TakeDamage(float amount)
    {
        if (isHittable)
        {
            health -= amount;
            //speed = 0;
            healthBar.fillAmount = health / startHealth;

            anim.SetBool("isWalk", false);
            anim.SetBool("isHit", true);

            GameObject bEffect = (GameObject)Instantiate(bloodEffect, transform.position, Quaternion.identity);
            Destroy(bEffect, 2f);

            if (health <= 0 && !isDead)
            {
                Die();
            }
        }
    }

    public void Slow(float amount, float duration)
    {
        speed = startSpeed * (1f - amount);
        timeManager.AddTimedAction(new TimedAction(() =>
        {
            speed = startSpeed;
        }, duration));
    }

    private void Die()
    {
        isDead = true;
        gameObject.tag = "Untagged";
        anim.SetTrigger("dead");
        anim.SetBool("isHit", false);
        mvt.enabled = false;
        //walk.Stop();
        //dead.PlayOneShot(soundDead);

        WaveSpawner.EnemiesAlive--;

        PlayerStats.addCurrency(value);
        enemyManager.EnemyDied(this);
    }

    public void ExitHitState()
    {
        speed = startSpeed;
        anim.SetBool("isWalk", true);
        anim.SetBool("isHit", false);
    }

}

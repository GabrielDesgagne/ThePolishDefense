using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class Enemy : MonoBehaviour
{

    public float startSpeed = 10;
    [HideInInspector]
    public float speed;
    public float startHealth = 100f;
    private float health;
    public int value = 50;

    //public GameObject deathEffect;
    public EnemyType type;

    public Image healthBar;
    [HideInInspector]
    public bool isDead = false;


    private Animator anim;
    public GameObject audioEnnemi;
    public AudioClip soundDead;
    private EnemyMovement mvt;
    private AudioSource walk;
    private AudioSource dead;
    //private float hitTime = 0;
    public GameObject uiDiedEffectPrefabs;
    private GameObject uiDied;
    private float speedMoveUi = 2f;
    public GameObject bloodEffect;
    //private float timeForHitAnimation=0.8f;
    [HideInInspector]
    public bool isHittable=false;

    private void Start() { Initialize(); }
    public void Initialize()
    {
        speed = startSpeed;
        health = startHealth;
        anim = GetComponent<Animator>();
        mvt = GetComponent<EnemyMovement>();
        walk = audioEnnemi.GetComponent<AudioSource>();
        dead = audioEnnemi.GetComponent<AudioSource>();
    }

    private void Update() { Refresh(); }

    public void Refresh()
    {

        //for test the take damage function. we can delete it after
        if (Input.GetKeyDown(KeyCode.A))
        {
            TakeDamage(50);
        }
        //
        if (isDead)
        {
            uiDied.transform.Translate(Vector3.up * speedMoveUi * Time.fixedDeltaTime, Space.World);
            Destroy(uiDied, 10f);
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        speed = 0;
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

    public void Slow(float amount)
    {
        speed = startSpeed * (1f - amount);
    }

    private void Die()
    {
        isDead = true;
        gameObject.tag = "Untagged";
        anim.SetTrigger("dead");
        anim.SetBool("isHit", false);
        mvt.enabled = false;
        walk.Stop();
        dead.PlayOneShot(soundDead);

        uiDied = Instantiate(uiDiedEffectPrefabs, transform.position, Quaternion.identity);


        WaveSpawner.EnemiesAlive--;
        //EnemyManager.Instance.EnemyDied(this);
        //will be do by enemyManager
        Destroy(gameObject, 10);
    }

    public void ExitHitState()
    {
        speed = startSpeed;
        anim.SetBool("isWalk", true);
        anim.SetBool("isHit", false);
    }

}

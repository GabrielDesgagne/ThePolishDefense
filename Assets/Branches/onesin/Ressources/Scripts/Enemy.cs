using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

    public float startSpeed = 10;
    [HideInInspector]
    public float speed;
    public float startHealth = 100f;
    private float health;
    public int value = 50;

    public GameObject deathEffect;

    public Image healthbar;

    private bool isDead = false;

    private void Start() { Initialize(); }

    void Initialize()
    {
        speed = startSpeed;
        health = startHealth;
    }

}

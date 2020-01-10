using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour {

    private Transform target;
    private int waypointIndex = 0;

    private Enemy enemy;

    private void Start() { Initialize(); }
    private void Update() { Refresh(); }
    private void FixedUpdate() { PhysicsRefresh(); }

    void Initialize()
    {
        enemy = GetComponent<Enemy>();
        target = Waypoints.points[0];
    }
    
    void PhysicsRefresh()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * enemy.speed * Time.fixedDeltaTime, Space.World);

    }
    
    void Refresh()
    {
        if (Vector3.Distance(transform.position, target.position) <= 0.3f)
        {
            //check the next way point
            if (waypointIndex >= Waypoints.points.Length - 1)
            {
                WaveSpawner.EnemiesAlive--;
                Destroy(gameObject);
                return;
            }

            waypointIndex++;
            target = Waypoints.points[waypointIndex];
        }

        enemy.speed = enemy.startSpeed;
    }


}

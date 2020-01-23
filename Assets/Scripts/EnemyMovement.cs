using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{

    private Transform target;
    private int waypointIndex = 0;

    private Enemy enemy;
    private Rigidbody rb;

    public void Initialize()
    {
        enemy = GetComponent<Enemy>();
        target = EnemyManager.Instance.waypoints[0];
        rb=GetComponent<Rigidbody>();
        //anim = GetComponent<Animator>();
    }

    public void PhysicsRefresh()
    {
        /*Vector3 dir = target.position - transform.position;
        transform.LookAt(target);
        //transform.Translate(dir.normalized * enemy.speed * Time.fixedDeltaTime, Space.World);
        if (!enemy.canEnter/*&&!anim.GetBool("isHit")*)
        {
            rb.MovePosition(dir.normalized * enemy.speed * Time.fixedDeltaTime + rb.position);
        }*/
        

    }

    public void Refresh()
    {
        if (Vector3.Distance(transform.position, target.position) <= 0.3f)
        {
            //check the next way point
            if (waypointIndex >= EnemyManager.Instance.waypoints.Length - 1)
            {
                PlayerStats.decrementHp();
                enemy.isHittable = false;
                WaveSpawner.EnemiesAlive--;
                EnemyManager.Instance.EnemyDied(enemy);
                //Destroy(gameObject);
                return;
            }

            waypointIndex++;
            target = EnemyManager.Instance.waypoints[waypointIndex];
        }

        if (Vector3.Distance(transform.position, MapVariables.instance.enemyStart.transform.position)>2f&&!enemy.canEnter){
            enemy.isHittable = true;
        }
       /* else
        {*/

        //}
    }


}

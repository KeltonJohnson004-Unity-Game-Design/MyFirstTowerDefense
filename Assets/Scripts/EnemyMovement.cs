
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyMovement : MonoBehaviour
{

    public Transform target = null;
    public int targetIndex = 0;
    public float distanceTraveled = 0f;
    private Vector3 direction = new Vector3(0f,0f,0f);
    private Enemy enemy;

    private void Start()
    {
        enemy = gameObject.GetComponent<Enemy>();
        if (target == null)
        {
            target = Waypoints.waypoints[targetIndex];
        }
    }
    void Update()
    {
        if(direction.magnitude < .5f)
        {
            GetDirection();
        }
        if (Vector3.Distance(target.position, transform.position) <= 0.15f)
        {
            GetNextTarget();
        }

        transform.Translate(direction * enemy.currentSpeed * Time.deltaTime, Space.World);
        distanceTraveled += enemy.currentSpeed * Time.deltaTime;


    }
    
    private void GetNextTarget()
    {
        if (targetIndex >= Waypoints.waypoints.Length - 1)
        {
            PathEnded();
            return;
        }

        targetIndex++;
        target = Waypoints.waypoints[targetIndex];

        GetDirection();

    }

    private void PathEnded()
    {
        PlayerStats.Lives -= enemy.enemyLevel;
        WaveSpawner.enemiesAlive--;
        Destroy(gameObject);
    }

    private void GetDirection()
    {
        direction = (target.position - transform.position).normalized;
    }

    public void setTarget(Transform target)
    {
        this.target = target;
    }
}

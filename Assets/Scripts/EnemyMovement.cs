
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyMovement : MonoBehaviour
{


    public float speed;

    public Transform target;
    public int targetIndex = 0;
    private Vector3 direction;

    private void Start()
    {
        target = Waypoints.waypoints[0];

        direction = (target.position - transform.position).normalized;
    }
    void Update()
    {
        if (Vector3.Distance(target.position, transform.position) <= 0.1f)
        {
            GetNextTarget();
        }
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
        
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

    }

    private void PathEnded()
    {
        Destroy(gameObject);
    }
}

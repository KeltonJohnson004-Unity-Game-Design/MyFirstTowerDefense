using UnityEngine;

public class TurretGeneral : MonoBehaviour
{
    private Transform target;
    //private GameObject targetEnemy;

    public float range;
    public float turnSpeed = 10f;
    public float fireRate = 1f;
    public int damage = 50;
    public float explosionRadius = 0f;
    private float fireCountdown = 0f;

    public Transform partToRotate;
    public GameObject bulletPrefab;
    public string enemyTag = "Enemy";

    public Transform firePoint;

    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, .2f);
    }

    private void Update()
    {
        fireCountdown -= Time.deltaTime;
        if (target == null)
            return;

        LockOnToTarget();

        if(fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1 / fireRate;
        }
    }
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float farthestTraveled = 0;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) < range)
            {
                float distanceTraveled = enemy.GetComponent<EnemyMovement>().distanceTraveled;
                if (distanceTraveled > farthestTraveled)
                {
                    farthestTraveled = distanceTraveled;
                    nearestEnemy = enemy;
                }
            }
        }

        if (nearestEnemy == null)
            target = null;
        else
            target = nearestEnemy.transform;
        //targetEnemy = nearestEnemy;
    }

    void LockOnToTarget()
    {
        Vector3 direction = target.position - transform.position;

        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(90f, rotation.y, 0f);
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        if(bullet!=null)
        {
            bullet.seek(target, damage, explosionRadius);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

}

using System;
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
    private BuildManager buildManager = BuildManager.instance;

    private Boolean validLocationToBuild = true;

    private Boolean isTowerBuild = false;
    private int towersInRange = 0;

    public GameObject canvas;
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


    private void OnMouseDrag()
    {

        Vector3 newPosition = new Vector3(1f,1f,1f);
        //Debug.Log(Input.mousePosition.x);


        //Debug.Log(buildManager.MaxPoints.position);
        Vector3 maxPoints = buildManager.MaxPoints.position;
        Vector3 minPoints = buildManager.MinPoints.position;
        Camera camera = buildManager.camera;

        float zPos = 0f;
        float xPos = 0f;

        if (camera.WorldToScreenPoint(minPoints).x < Input.mousePosition.x)
        {
            zPos = minPoints.z;
        }
        else if (camera.WorldToScreenPoint(maxPoints).x > Input.mousePosition.x)
        {
            zPos = maxPoints.z;
        }
        else
        {
            float percent = (Input.mousePosition.x - camera.WorldToScreenPoint(maxPoints).x) / (camera.WorldToScreenPoint(minPoints).x - camera.WorldToScreenPoint(maxPoints).x);
            zPos = percent * (minPoints.z - maxPoints.z) + maxPoints.z;
        }

        if (camera.WorldToScreenPoint(minPoints).y > Input.mousePosition.y)
        {
            xPos = minPoints.x;
        }
        else if (camera.WorldToScreenPoint(maxPoints).y < Input.mousePosition.y)
        {
            xPos = maxPoints.x;
        }
        else
        {
            float percent = (Input.mousePosition.y - camera.WorldToScreenPoint(maxPoints).y) / (camera.WorldToScreenPoint(minPoints).y - camera.WorldToScreenPoint(maxPoints).y);
            xPos = percent * (minPoints.x - maxPoints.x) + maxPoints.x;
        }



        transform.position = new Vector3(xPos, gameObject.transform.position.y, zPos);
    }

    private void OnMouseDown()
    {
        Debug.Log("Mouse Clicked");
    }

    private void OnMouseUp()
    {
        Debug.Log(towersInRange);
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
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
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

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Entered");
        if (other.tag == "Towers" || other.tag == "Path")
        {
            towersInRange++;
            validLocationToBuild = false;

            canvas.SetActive(true);

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Towers" || other.tag == "Path")
        {
            towersInRange--;
            if(towersInRange <= 0)
            {
                towersInRange = 0;
                validLocationToBuild = true;
                canvas.SetActive(false);
            }
        }
    }
}

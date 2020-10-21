using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform target;
    public float speed = 50f;
    private float explosionRadius = 0f;
    private float damage = 0;

    private bool targetSet = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!targetSet)
            return;
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if(direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }
        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    private void HitTarget()
    {
        if(explosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            DamageEnemy(target);
        }
        Destroy(gameObject);
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach(Collider collider in colliders)
        {
            if(collider.tag == "Enemy")
            {
                DamageEnemy(collider.transform);
            }
        }
    }

    private void DamageEnemy(Transform target)
    {
        Enemy e = target.GetComponent<Enemy>();
        if (e != null)
        {
            e.takeDamage(damage);
        }
    }

    public void seek(Transform _target, float _damage, float _explosionRadius)
    {
        target = _target;
        damage = _damage;
        explosionRadius = _explosionRadius;
        targetSet = true;
    }
}

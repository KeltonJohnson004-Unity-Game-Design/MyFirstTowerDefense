using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float currentSpeed;
    public Transform target;
    public float health;
    public int enemyLevel;
    private float baseSpeed;
    private float baseHealth;
    private bool isDead = false;

    private void Start()
    {
        baseSpeed = currentSpeed;
        baseHealth = health;
    }
    void Update()
    {
        currentSpeed = baseSpeed;
    }
    public void takeDamage(float damage)
    {

        if (isDead)
            return;


        if (damage >= health)
        {
            Die();
        }
        else
        {
            health -= damage;
        }
    }

    private void Die()
    {
        PlayerStats.Money += enemyLevel;
        isDead = true;
        Destroy(gameObject);
        WaveSpawner.enemiesAlive--;

    }

    public void Slow(float rate)
    {
        currentSpeed = baseSpeed * (1 - rate);
    }

}

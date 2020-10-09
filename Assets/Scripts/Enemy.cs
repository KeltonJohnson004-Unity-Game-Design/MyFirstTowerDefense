using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float currentSpeed;
    public Transform target;
    public int health;
    public int enemyLevel;
    private float baseSpeed;
    private int baseHealth;
    private bool isDead = false;

    private void Start()
    {
        baseSpeed = currentSpeed;
        baseHealth = health;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            takeDamage(75);
        }
    }

    public void takeDamage(int damage)
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
            if((float)health / (float)baseHealth <= .5f)
            {
                baseSpeed = baseSpeed * .65f;

                currentSpeed = baseSpeed;
            }
        }
    }

    private void Die()
    {
        PlayerStats.Money += enemyLevel;
        isDead = true;
        Destroy(gameObject);
        WaveSpawner.enemiesAlive--;

    }

}

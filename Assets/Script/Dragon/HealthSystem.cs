using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public float Health = 100f;
    private float currentHealth;

    void Start()
    {
        currentHealth = Health;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Entity died!");
        Destroy(gameObject);
    }
}

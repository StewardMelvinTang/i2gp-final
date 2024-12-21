using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 10f; // Damage dealt by the bullet
    public float lifetime = 5f; // Time before the bullet disappears

    void Start()
    {
        Destroy(gameObject, lifetime); // Destroy the bullet after a certain time
    }

    void OnCollisionEnter(Collision collision)
    {
        HealthSystem dragon = collision.collider.GetComponent<HealthSystem>();
        if (dragon != null)
        {
            dragon.TakeDamage(damage); // Apply damage to the dragon
        }

        Destroy(gameObject); // Destroy the bullet upon collision
    }
}
